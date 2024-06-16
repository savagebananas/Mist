using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] public string name;
    [SerializeField] public string description;
    [SerializeField] public int maxStackSize;
    [SerializeField] public Sprite itemImage;

    [Header("Prefab must include DroppedItem.cs")]
    public GameObject droppedItem;

    [Header("Prefab must include IEquippable.cs")]
    public GameObject equippedItem;
}
