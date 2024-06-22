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
    private ItemData itemData;
    private int equippedIndex = -1;

    public void EquipItem(int index)
    {
        if (equippedIndex == index) return;

        // Destroy existing equipped item
        if (equippedItem != null) GameObject.Destroy(equippedItem);

        // If equipped slot is empty, pull out hands
        if (inventoryHotbar.iSlots[index].itemData == null)
        {
            equippedItem = null;
            itemData = null;
            equippedIndex = index;
            return;
        }

        // Equip valid item
        GameObject equippable = Instantiate(inventoryHotbar.iSlots[index].itemData.equippedItem, hands);
        equippedItem = equippable;
        itemData = inventoryHotbar.iSlots[index].itemData;
        equippedIndex = index;
        equippable.GetComponent<IEquippable>().SetInventories(inventoryHotbar, inventoryMain);
    }

    public void DestroyEquipped()
    {
        GameObject.Destroy(equippedItem);
    }

    public void DropCurrentItem()
    {
        if (equippedItem != null)
        {
            var obj = Instantiate(itemData.droppedItem, transform.position, Quaternion.identity);
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
