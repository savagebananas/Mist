using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IEquippable
{



    public void UseItem()
    {
        Debug.Log("Eat ");
    }

    public void StopUseItem()
    {

    }

    public void DropItem(InventoryData hotbar, ItemSpawnManager itemSpawnManager)
    {
        throw new System.NotImplementedException();
    }

    public void SetInventories(InventoryData inventoryHotbar, InventoryData inventoryMain)
    {

    }

}
