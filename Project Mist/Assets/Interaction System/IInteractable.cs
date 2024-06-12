using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Called when the player interacts with an interactable
    /// </summary>
    public void OnInteract(PlayerInteractor player);

    /// <summary>
    /// Called when player finishes the interaction
    /// </summary>
    public void OnInteractExit();
}
