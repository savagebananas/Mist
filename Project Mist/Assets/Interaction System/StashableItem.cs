using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StashableItem : MonoBehaviour, IInteractable
{
    public void OnInteract()
    {
        Debug.Log("Pickup item");
        Destroy(gameObject);
    }

    public void OnInteractExit()
    {
        throw new System.NotImplementedException();
    }

}
