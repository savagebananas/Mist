using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] InventorySlotUI slotUIPrefab;
    [SerializeField] InventoryData inventory;
    [SerializeField] InventoryMouseSlotUI mouseSlotUI;

    private Dictionary<InventorySlotUI, InventorySlot> slotDictionary;

    /// <summary>
    /// Sets up UI for one inventory.
    /// Creates dictionary of corresponding slots and their UI equivalent.
    /// </summary>
    public void InitializeUI()
    {
        // Set up listener for inventory changed event
        inventory.OnInventoryChanged.AddListener(UpdateUI);
 
        slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

        for (int i = 0; i < inventory.GetInventorySize(); i++)
        {
            var uiSlot = Instantiate(slotUIPrefab, transform);
            uiSlot.parentDisplay = this;    
            slotDictionary.Add(uiSlot, inventory.iSlots[i]);
        }
    }

    /// <summary>
    /// All slot button clicks will lead here.
    /// Logic for transfering items between mouse and slot.
    /// </summary>
    /// <param name="slotUI"> The slot being clicked </param>
    public void SlotClicked(InventorySlotUI slotUI)
    {
        InventorySlot slot = slotDictionary[slotUI];

        // Case 1: mouse empty, slot has item
        if (mouseSlotUI.SlotEmpty() && !slotUI.SlotEmpty())
        {
            Debug.Log("Case 1");
            // Move data to mouse slot
            mouseSlotUI.SetItem(slot.itemData, slot.quantity);

            // Clear backend and front end slots
            slotUI.ClearSlotVisuals();
            slot.ClearSlot();
        }

        else if (!mouseSlotUI.SlotEmpty())
        {
            // Case 2: mouse has items, slot empty
            if (slotUI.SlotEmpty())
            {
                Debug.Log("Case 2");
                // Move items mouse -> slot
                slot.itemData = mouseSlotUI.GetItemData();
                slot.quantity = mouseSlotUI.GetQuantity();
                slotUI.UpdateUISlot(slot);
                mouseSlotUI.Clear(); // remove items from mouse
            }
            // Case 3: Both mouse and slot have items
            else if (!slotUI.SlotEmpty())
            {
                // Case 3.1: Different items, swap mouse and slot
                if (slot.itemData != mouseSlotUI.GetItemData())
                {
                    Debug.Log("Case 3.1");
                    ItemData tempData = slot.itemData;
                    int tempQuantity = slot.quantity;

                    slot.itemData = mouseSlotUI.GetItemData();
                    slot.quantity = mouseSlotUI.GetQuantity();
                    slotUI.UpdateUISlot(slot);
                    mouseSlotUI.SetItem(tempData, tempQuantity);
                }

                // Case 3.2: Same items for mouse and slot
                else
                {
                    Debug.Log("Case 3.2");
                    slot.AddAmount(mouseSlotUI.GetQuantity(), out int remaining);
                    slotUI.UpdateUISlot(slot);
                    if (remaining == 0) return;
                    mouseSlotUI.SetQuantity(remaining);
                }
            }
        }



    }

    /// <summary>
    /// Updates all inventory slots that have changed.
    /// Called through event "OnInventoryChanged" of InventoryData class.
    /// </summary>
    protected void UpdateUI()
    {
        foreach (var slot in slotDictionary)
        {
            slot.Key.UpdateUISlot(slot.Value);
        }
    }
}
