using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject inventoryCanvas;

    private void Awake()
    {
        // Initialize all inventory UI (main inventory and hotbar)
        InventoryUI[] inventoryUIs = inventoryCanvas.GetComponentsInChildren<InventoryUI>();
        foreach (InventoryUI i in inventoryUIs)
        {
            i.InitializeUI();
        }
    }

    #region Inventory UI

    /// <summary>
    /// Turns on/off inventory UI
    /// </summary>
    public void UpdateInventoryState()
    {
        if (inventoryCanvas.active)
        {
            CloseInventory();
        }
        else OpenInventory();
    }

    private void OpenInventory()
    {
        Cursor.lockState = CursorLockMode.Confined;
        inventoryCanvas.SetActive(true);
    }

    private void CloseInventory()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inventoryCanvas.SetActive(false);
    }
    
    #endregion
}
