using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMouseSlotUI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCountText;

    private ItemData item;
    private int quantity;

    private Vector2 offset = new Vector2 (10, -10);

    bool isActive = false;
    
    private void Update()
    {
        if (isActive)
        {
            transform.position = Input.mousePosition + (Vector3) offset;
            if (item == null) SetInactive();
        }

        
    }

    public void SetItem(ItemData itemData, int amt)
    {
        this.item = itemData;
        quantity = amt;
        UpdateVisuals();
        SetActive();
    }

    private void UpdateVisuals()
    {
        itemSprite.sprite = item.itemImage;
        itemCountText.text = quantity.ToString();
    }

    /// <summary>
    /// Update Visuals and make slot item appear
    /// </summary>
    public void SetActive()
    {
        itemSprite.color =  Color.white;
        isActive = true;
    }

    public void SetInactive()
    {
        itemSprite.color = Color.clear;
        isActive = true;
    }

    public void Clear()
    {
        // Clear data
        item = null;
        quantity = 0;

        // Clear visuals
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;
        itemCountText.text = "";

        SetInactive();
    }

    public bool SlotEmpty()
    {
        return item == null;
    }

    public ItemData GetItemData()
    {
        return item;
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
}
