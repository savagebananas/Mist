using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInteractor))]
public class PlayerInput : MonoBehaviour
{
    private PlayerInteractor playerInteraction;

    void Start()
    {
        playerInteraction = GetComponent<PlayerInteractor>();
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
