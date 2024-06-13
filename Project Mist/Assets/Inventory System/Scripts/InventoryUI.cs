using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventorySlotUI slotUIPrefab;

    public InventoryData inventory;

    [SerializeField] Dictionary<InventorySlotUI, InventorySlot> slotDictionary;

    void Start()
    {
        InitializeUI();
    }

    /// <summary>
    /// Sets up UI for one inventory.
    /// Creates dictionary of corresponding slots and their UI equivalent.
    /// </summary>
    private void InitializeUI()
    {
        // Set up listener for inventory changed event
        inventory.OnInventoryChanged.AddListener(UpdateUI);
 
        slotDictionary = new Dictionary<InventorySlotUI, InventorySlot>();

        for (int i = 0; i < inventory.GetInventorySize(); i++)
        {
            var uiSlot = Instantiate(slotUIPrefab, transform);
            slotDictionary.Add(uiSlot, inventory.iSlots[i]);
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
