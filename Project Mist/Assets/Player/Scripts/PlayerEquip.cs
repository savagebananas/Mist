using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    [SerializeField] private InventoryData hotbarInventory;

    [SerializeField] private Transform hands;

    private GameObject equippedItem;

    private int equippedIndex = -1;

    public void EquipItem(int index)
    {
        Debug.Log("Equip");
        // Equip the same item already equipped
        if (equippedIndex == index) return;

        if (equippedItem != null) GameObject.Destroy(equippedItem);

        if (hotbarInventory.iSlots[index].itemData == null)
        {
            equippedItem = null;
            equippedIndex = -1;
            return;
        }

        GameObject equippable = Instantiate(hotbarInventory.iSlots[index].itemData.equippedItem, hands);
        equippedItem = equippable;
        equippedIndex = index;
    }
}
