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
}
