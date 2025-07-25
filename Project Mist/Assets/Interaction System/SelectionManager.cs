using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of current interactable which the player is looking at
/// </summary>
public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string interactableTag = "Interactable";

    public float interactionDistance;
    private Transform currentSelection;

    void Update()
    {
        // Important: ViewportPointToRay instead of ScreenToPointRay(mousePos) b/c of renderTexture
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // Shoot Raycast
        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;

            // DESELECT: Raycast Hit but it is a different object from current
            if (selection != currentSelection && currentSelection != null) DeselectCurrent();

            // Interaction Distance Checks
            if (Vector3.Distance(selection.position, transform.position) > interactionDistance) return;

            // Check if object is interactable
            if (selection.CompareTag(interactableTag))
            {
                // Highlight object
                HighlightObject highlightObject = selection.GetComponent<HighlightObject>();
                highlightObject?.Invoke(nameof(highlightObject.Highlight), 0);

                currentSelection = selection;
            }
        }

        // DESELECT: Raycast did not hit & an item is selected
        else if (currentSelection != null)
        {
            DeselectCurrent();
        }
    }

    /// <summary>
    /// Remove "able to interact" visuals and dereference currentSelection
    /// </summary>
    private void DeselectCurrent()
    {
        HighlightObject highlightObject = currentSelection.GetComponent<HighlightObject>();

        highlightObject?.Invoke(nameof(highlightObject.DeHighlight), 0);

        currentSelection = null;
    }

    public GameObject GetSelectedObject()
    {
        return currentSelection.gameObject;
    }

    public bool HasSelectedObject()
    {
        return currentSelection != null;
    }

    private void OnDrawGizmos()
    {
        if (HasSelectedObject())
        {
            Gizmos.color = Color.green;
        }
        else Gizmos.color = Color.yellow;

        Gizmos.DrawRay(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)));

    }
}
