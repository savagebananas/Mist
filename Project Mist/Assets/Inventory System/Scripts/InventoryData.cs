using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class InventoryData : ScriptableObject
{
    [SerializeField]
    public List<InventorySlot> iSlots = new List<InventorySlot>();

    public UnityEvent OnInventoryChanged;

    public void AddToInventory(ItemData item, int amountToAdd)
    {
        int amt = amountToAdd;
        int maxStackSize = item.maxStackSize;

        // Find all matching items in inventory
        for (int i = 0; i < iSlots.Count; i++)
        {
            // There is a matching item
            if (iSlots[i].itemData == item)
            {
                // Case 1: Can fully stack item on pre-existing
                if (iSlots[i].HasEnoughRoomLeftInStack(amt))
                {
                    iSlots[i].AddAmount(amt);
                    OnInventoryChanged.Invoke(); // Call event
                    return;
                }
                // Case 2: Can partially stack item
                else if (!iSlots[i].IsFull())
                {
                    int amtToFull = maxStackSize - iSlots[i].quantity;
                    iSlots[i].AddAmount(amtToFull);
                    amt -= amtToFull;
                }
                // Else: item slot is full
            }
        }

        // Find Empty Slot
        for (int i = 0; i < iSlots.Count; i++)
        {
            if (amt <= 0)
            {
                OnInventoryChanged.Invoke(); // Call event
                return;
            }

            // Empty Slot
            if (iSlots[i].itemData == null)
            {
                // Case 1: Can fully stack item on empty slot
                if (amt < item.maxStackSize)
                {
                    iSlots[i].itemData = item;
                    iSlots[i].AddAmount(amt);
                    OnInventoryChanged.Invoke(); // Call event
                    return;
                }
                // Case 2: Can partially stack item on empty slot
                else
                {
                    iSlots[i].itemData = item;
                    int amtToFull = maxStackSize - iSlots[i].quantity;
                    iSlots[i].AddAmount(amtToFull);
                    amt -= amtToFull;
                }
            }
        }

        Debug.LogError("Inventory Full, Can't add " + amt + " of " + item.name);
    }

    public int GetInventorySize()
    {
        return iSlots.Count;
    }

    public void ClearInventory()
    {
        foreach (InventorySlot slot in iSlots)
        {
            slot.ClearSlot();
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemData itemData;
    public int quantity;

    public InventorySlot(ItemData itemData, int amt)
    {
        this.itemData = itemData;
        this.quantity = amt;
    }

    public bool IsFull()
    {
        return quantity == itemData.maxStackSize;
    }

    public bool HasEnoughRoomLeftInStack(int amtToAdd)
    {
        return quantity + amtToAdd < itemData.maxStackSize;
    }

    public void AddAmount(int amt)
    {
        quantity += amt;
        CheckForError();
    }

    public void Remove(int amt)
    {
        quantity -= amt;
        if (quantity <= 0) ClearSlot();
        CheckForError();
    }

    public void ClearSlot()
    {
        itemData = null;
        quantity = 0;
    }

    private void CheckForError()
    {
        if (quantity > itemData.maxStackSize || quantity < 0) Debug.LogError("Item amt wrong!");
        if (itemData == null && quantity != 0) Debug.LogError("Quantity is not 0 for null item");
    }

}
