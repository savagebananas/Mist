using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    [SerializeField] private InventoryData inventoryHotbar;
    [SerializeField] private InventoryData inventoryMain;

    [SerializeField] private Transform hands;

    private GameObject equippedItem;
    private ItemData equippedItemData;
    private int equippedIndex = -1;

    private void Awake()
    {
        inventoryHotbar.OnInventoryChanged.AddListener(OnHotbarUpdate);
    }

    public void EquipItem(int index)
    {
        if (equippedIndex == index) return;

        // Destroy existing equipped item
        if (equippedItem != null) GameObject.Destroy(equippedItem);

        // If equipped slot is empty, pull out hands
        if (inventoryHotbar.iSlots[index].itemData == null)
        {
            equippedItem = null;
            equippedItemData = null;
            equippedIndex = index;
            return;
        }

        // Equip valid item
        GameObject equippable = Instantiate(inventoryHotbar.iSlots[index].itemData.equippedItem, hands);
        equippedItem = equippable;
        equippedItemData = inventoryHotbar.iSlots[index].itemData;
        equippable.GetComponent<IEquippable>().SetInventories(inventoryHotbar, inventoryMain);

        equippedIndex = index;
    }
    
    public void OnHotbarUpdate()
    {
        if (equippedIndex == -1) return;
        // if equipped hotbar item is removed, dequip item
        if (inventoryHotbar.iSlots[equippedIndex].itemData == null)
        {
            equippedIndex = -1;
            DestroyEquipped();
        }
    }

    /// <summary>
    /// Destroy phyiscal equipped item
    /// </summary>
    public void DestroyEquipped()
    {
        GameObject.Destroy(equippedItem);
    }

    /// <summary>
    /// Remove item from hotbar inventory and destroy equipped item gameobject
    /// </summary>
    public void DropCurrentItem()
    {
        if (equippedItem != null)
        {
            // Generate physical item in the world, set itemData 
            var obj = Instantiate(equippedItemData.droppedItem, transform.position, Quaternion.identity);
            obj.GetComponent<DroppedItem>().SetQuantity(inventoryHotbar.iSlots[equippedIndex].quantity);

            // Remove from inventory and destroy equipped item
            inventoryHotbar.RemoveFromInventory(equippedIndex);
            DestroyEquipped();
        }
    }

    public void UseItem(string key)
    {
        if (equippedItem == null) return;

        IEquippable item = equippedItem.GetComponent<IEquippable>();
        item.UseItem(key);
    }
}
