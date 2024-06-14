using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DroppedItem : MonoBehaviour, IInteractable
{
    [SerializeField] ItemData itemData;
    [SerializeField] int quantity;

    public void OnInteract(PlayerInteractor player)
    {
        Debug.Log("Pickup item: " + itemData.name);
        player.GetComponentInChildren<InventoryHolder>().inventory.AddToInventory(itemData, quantity);
        Destroy(gameObject);
    }

    public void OnInteractExit()
    {
        throw new System.NotImplementedException();
    }

    public void SetQuantity(int quantity)
    {
        this.quantity = quantity;
    }

}
