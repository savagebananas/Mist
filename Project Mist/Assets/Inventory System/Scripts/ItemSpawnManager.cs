using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField] Transform player;

    public void SpawnItemOnPlayer(ItemData itemData, int amt)
    {
        var obj = Instantiate(itemData.droppedItem, player.transform.position, Quaternion.identity);
        obj.GetComponent<DroppedItem>().SetQuantity(amt);
    }
}
