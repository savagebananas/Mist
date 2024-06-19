using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    [SerializeField] private InventoryData hotbarInventory;

    [SerializeField] private Transform hands;

    private GameObject equippedItem;
    private ItemData itemData;
    private int equippedIndex = -1;

    public void EquipItem(int index)
    {
        // Equip the same item already equipped
        if (equippedIndex == index) return;

        if (equippedItem != null) GameObject.Destroy(equippedItem);

        if (hotbarInventory.iSlots[index].itemData == null)
        {
            equippedItem = null;
            itemData = null;
            equippedIndex = index;
            return;
        }

        GameObject equippable = Instantiate(hotbarInventory.iSlots[index].itemData.equippedItem, hands);
        equippedItem = equippable;
        itemData = hotbarInventory.iSlots[index].itemData;
        equippedIndex = index;
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
            obj.GetComponent<DroppedItem>().SetQuantity(hotbarInventory.iSlots[equippedIndex].quantity);

            // Remove from inventory and destroy equipped item
            hotbarInventory.RemoveFromInventory(equippedIndex);
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
