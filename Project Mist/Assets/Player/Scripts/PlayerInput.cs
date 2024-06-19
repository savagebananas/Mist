using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(PlayerInteractor), typeof(PlayerEquip))]
public class PlayerInput : MonoBehaviour
{
    public static bool active = true;
    public bool inventoryOpen = false;

    private PlayerInteractor playerInteraction;
    private PlayerLook playerLook;
    private PlayerEquip playerEquip;

    public UnityEvent ChangeInventoryUIState;

    void Awake()
    {
        playerInteraction = GetComponent<PlayerInteractor>();
        playerLook = GetComponent<PlayerLook>();
        playerEquip = GetComponent<PlayerEquip>();

        ChangeInventoryUIState.AddListener(GameObject.Find("UIController").GetComponent<UIController>().UpdateInventoryState);
    }

    void Update()
    {
        if (!active) return;

        if (Input.GetButtonDown("Interact"))
        {
            playerInteraction.Interact();
        }

        // Open Inventory
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeInventoryUIState.Invoke();
            playerLook.ChangeActiveState();
            inventoryOpen = !inventoryOpen;
        }

        // =============================
        // Hotbar and equipment
        // =============================

        if (inventoryOpen) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerEquip.EquipItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerEquip.EquipItem(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerEquip.EquipItem(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerEquip.EquipItem(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playerEquip.EquipItem(4);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            // Drop item
            playerEquip.DropCurrentItem();
        }

        // Use item
        if (Input.GetMouseButtonDown(0))
        {
            // Drop item
            playerEquip.UseItem("LeftClick");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Drop item
            playerEquip.UseItem("R");
        }


    }
}
