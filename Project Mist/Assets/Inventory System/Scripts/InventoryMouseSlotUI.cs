using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryMouseSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCountText;

    private ItemData itemData;
    private int quantity;

    private Vector2 offset = new Vector2 (10, -10);

    bool isActive = false;

    public ItemSpawnManager itemSpawnManager;
    public PlayerEquip playerEquip;

    private void Update()
    {

        if (itemData != null)
        {
            // Follow mouse
            transform.position = Input.mousePosition + (Vector3) offset;
            
            if (Input.GetMouseButtonDown(0))
            {
                // If not over UI
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    // Drop item
                    itemSpawnManager.SpawnItem(itemData, quantity);
                    playerEquip.DestroyEquipped();
                    Clear();
                }
            }
        }

        
    }

    public void SetItem(ItemData itemData, int amt)
    {
        this.itemData = itemData;
        quantity = amt;
        UpdateVisuals();
    }

    public void Clear()
    {
        // Clear data
        itemData = null;
        quantity = 0;
        UpdateVisuals();
    }

    public bool SlotEmpty()
    {
        return itemData == null;
    }

    public ItemData GetItemData()
    {
        return itemData;
    }

    public int GetQuantity()
    {
        return quantity;
    }

    public void SetQuantity(int quantity)
    {
        this.quantity = quantity;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (itemData == null)
        {
            // Clear visuals
            itemSprite.color = Color.clear;
            itemSprite.sprite = null;
            itemCountText.text = "";
            return;
        }

        // Else
        itemSprite.color = Color.white;
        itemSprite.sprite = itemData.itemImage;
        itemCountText.text = quantity.ToString();
    }
}
