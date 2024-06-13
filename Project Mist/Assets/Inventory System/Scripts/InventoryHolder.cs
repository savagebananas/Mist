using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public InventoryData inventory;
    public ItemData testItem;

    private void Start()
    {
        inventory.ClearInventory();
    }
}
