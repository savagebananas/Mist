using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquippable
{

    public void UseItem();

    public void DropItem(InventoryData hotbar, ItemSpawnManager itemSpawnManager);
}
