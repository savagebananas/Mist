using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IEquippable
{
    [SerializeField] private int damage;

    public void UseItem()
    {
        // Animate Swing
        Debug.Log("Swing");

    }

    public void DropItem(InventoryData hotbar, ItemSpawnManager itemSpawnManager)
    {
        throw new System.NotImplementedException();
    }


}
