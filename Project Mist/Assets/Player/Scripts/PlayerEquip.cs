using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages player equipment (5 item slots)
/// Functionality to equip hand held items and dropping held items
/// </summary>
public class PlayerEquip : MonoBehaviour
{
    [SerializeField] private InventoryData inventoryHotbar;
    [SerializeField] private InventoryData inventoryMain;

    [SerializeField] private Transform hands;
    [SerializeField] private Transform dropTransform;

    private GameObject equippedItem;
    private ItemData equippedItemData;
    private IEquippable currentIEquippable;
    private int equippedIndex = -1;

    private void Awake()
    {
        inventoryHotbar.OnInventoryChanged.AddListener(OnHotbarUpdate);
    }

    public void EquipItem(int index)
    {
        if (equippedIndex == index) return;

        // Destroy existing equipped item
        if (equippedItem != null) GameObject.Destroy(equippedItem);

        // If equipped slot is empty, do nothing
        if (inventoryHotbar.iSlots[index].itemData == null)
        {
            equippedItem = null;
            equippedItemData = null;
            equippedIndex = -1;
            currentIEquippable = null;
            return;
        }

        // Equip valid item
        GameObject equippable = Instantiate(inventoryHotbar.iSlots[index].itemData.equippedItem, hands);
        equippedItem = equippable;
        equippedItemData = inventoryHotbar.iSlots[index].itemData;
        equippedIndex = index;
        currentIEquippable = equippedItem.GetComponent<IEquippable>();
    }

    /// <summary>
    /// Called when the hotbar inventory has changed (Ex: item is removed)
    /// </summary>
    public void OnHotbarUpdate()
    {
        if (equippedIndex == -1) return;
        // if equipped hotbar item is removed, dequip item
        if (inventoryHotbar.iSlots[equippedIndex].itemData == null)
        {
            equippedIndex = -1;
            DestroyEquipped();
        }
    }

    /// <summary>
    /// Destroy phyiscal equipped item
    /// </summary>
    public void DestroyEquipped()
    {
        GameObject.Destroy(equippedItem);
    }

    /// <summary>
    /// Remove item from hotbar inventory and destroy equipped item gameobject
    /// </summary>
    public void DropCurrentItem()
    {
        if (equippedItem != null)
        {
            // Generate physical item in the world, set itemData 
            var obj = Instantiate(equippedItemData.droppedItem, hands.position, Quaternion.identity);
            obj.GetComponent<DroppedItem>().SetQuantity(inventoryHotbar.iSlots[equippedIndex].quantity);
            Vector3 dir = Vector3.Normalize(dropTransform.position - hands.position);
            obj.transform.rotation = equippedItem.transform.rotation;
            obj.GetComponent<Rigidbody>().AddForce(dir * 4f, ForceMode.Impulse);

            // Remove from inventory and destroy equipped item
            inventoryHotbar.RemoveFromInventory(equippedIndex);
            equippedIndex = -1;
            DestroyEquipped();
        }
    }

    /// <summary>
    /// Called when player starts holding down the use item button
    /// </summary>
    public void UseItem()
    {
        if (equippedItem == null) return;
        currentIEquippable.UseItem();
    }

    /// <summary>
    /// Called when player stops holding down the use item button
    /// </summary>
    public void StopUseItem()
    {
        if (equippedItem == null) return;

        IEquippable item = equippedItem.GetComponent<IEquippable>();
        item.StopUseItem();
    }

    public InventoryData GetMainInventory()
    {
        return inventoryMain;
    }

    public InventoryData GetHotbarInventory()
    {
        return inventoryHotbar;
    }
}
