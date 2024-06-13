using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(PlayerInteractor))]
public class PlayerInput : MonoBehaviour
{
    private PlayerInteractor playerInteraction;
    private PlayerLook playerLook;

    public UnityEvent ChangeInventoryUIState;

    void Awake()
    {
        playerInteraction = GetComponent<PlayerInteractor>();
        playerLook = GetComponent<PlayerLook>();
        ChangeInventoryUIState.AddListener(GameObject.Find("UIController").GetComponent<UIController>().UpdateInventoryState);
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            playerInteraction.Interact();
        }

        // Open Inventory
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeInventoryUIState.Invoke();
            playerLook.ChangeActiveState();
        }
    }
}
