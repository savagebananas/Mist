using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInteraction))]
public class PlayerInput : MonoBehaviour
{
    private PlayerInteraction playerInteraction;

    void Start()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            playerInteraction.Interact();
        }
    }
}
