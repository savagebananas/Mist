using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryData inventory;
    public ItemData testItem;

    Action OnInventoryChanged;

    private void Start()
    {
        inventory.InitializeSlots();
    }
}
