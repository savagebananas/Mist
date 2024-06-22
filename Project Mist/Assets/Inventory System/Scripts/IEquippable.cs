using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquippable
{
    public void UseItem(string key);

    public void DropItem(InventoryData hotbar, ItemSpawnManager itemSpawnManager);

    public void SetInventories(InventoryData inventoryHotbar, InventoryData inventoryMains);
}
