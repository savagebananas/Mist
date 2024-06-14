using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public InventoryData inventory;

    private void Start()
    {
        inventory.ClearInventory();
    }
}
