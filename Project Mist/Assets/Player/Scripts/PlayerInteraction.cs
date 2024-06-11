using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelectionManager))]
public class PlayerInteraction : MonoBehaviour
{
    SelectionManager selectionManager;

    private void Awake()
    {
        selectionManager = GetComponent<SelectionManager>();
    }

    public void Interact()
    {
        IInteractable interactable = selectionManager.GetSelectedObject().GetComponent<IInteractable>();
        if (interactable == null) Debug.LogError("Selected Object has no IInteractable!");

        interactable.OnInteract();
    }
}
