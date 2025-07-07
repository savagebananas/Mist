using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionManager))]
public class PlayerInteractor : MonoBehaviour
{
    SelectionManager selectionManager; // Selection Manager has the interactable that the player is looking at

    private void Awake()
    {
        selectionManager = GetComponent<SelectionManager>();
    }

    public void Interact()
    {
        // Check for available interactable object
        if (!selectionManager.HasSelectedObject()) return;

        IInteractable interactable = selectionManager.GetSelectedObject().GetComponent<IInteractable>();
        if (interactable == null)
        {
            Debug.LogError("Selected Object has no IInteractable!");
            return;
        }

        interactable.OnInteract(this);
    }
}
