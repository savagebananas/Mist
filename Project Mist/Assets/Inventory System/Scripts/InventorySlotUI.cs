using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;


public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] ItemData itemData;
    [SerializeField] int quantity;

    InventorySlot slotBackend;

    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCountText;


    public void UpdateUISlot(InventorySlot slot)
    {
        // if nothing changed, exit
        if (itemData == slot.itemData && quantity == slot.quantity) return;

        // else, update slot
        itemData = slot.itemData;
        quantity = slot.quantity;
        UpdateSlotVisuals();
    }

    private void UpdateSlotVisuals()
    {
        itemSprite.sprite = itemData.itemImage;
        itemSprite.color = Color.white;
        itemCountText.text = quantity.ToString();
    }

    public void ClearUISlot()
    {
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;
        itemCountText.text = "";
        slotBackend.ClearSlot();
    }

}
