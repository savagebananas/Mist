using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemData itemData;

    public void OnInteract(PlayerInteractor player)
    {
        Debug.Log("Pickup item");
        player.GetComponentInChildren<InventoryManager>().inventory.AddToInventory(itemData, 1);
        Destroy(gameObject);
    }

    public void OnInteractExit()
    {
        throw new System.NotImplementedException();
    }

}
