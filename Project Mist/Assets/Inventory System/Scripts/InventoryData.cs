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
                iSlots[i].AddAmount(amt, out int remaining);

                if (remaining == 0)
                {
                    OnInventoryChanged.Invoke();
                    return;
                }
                amt = remaining;
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
                iSlots[i].itemData = item;
                iSlots[i].AddAmount(amt, out int remaining);
                
                if (remaining == 0)
                {
                    OnInventoryChanged.Invoke();
                    return;
                }

                amt = remaining;
            }
        }

        Debug.LogError("Inventory Full, Can't add " + amt + " of " + item.name);
    }

    public void RemoveFromInventory(InventorySlot slot)
    {
        slot.ClearSlot();
        OnInventoryChanged.Invoke();
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
        OnInventoryChanged.Invoke();
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

    public void AddAmount(int amt, out int remaining)
    {
        // Adding will cause overflow
        if (itemData.maxStackSize - quantity < amt)
        {
            int amtToAdd = itemData.maxStackSize - quantity;
            quantity += amtToAdd;
            remaining = amt - amtToAdd;
        }
        else
        {
            quantity += amt;
            remaining = 0;
        }

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
