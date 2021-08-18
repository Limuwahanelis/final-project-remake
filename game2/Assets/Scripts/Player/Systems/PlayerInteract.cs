using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private IInteractable objectToInteract;

    public void setObjectToInteract(IInteractable obj)
    {
        objectToInteract = obj;
    }
    public void RemoveObjectToInteract()
    {
        objectToInteract = null;
    }
    public void InteractWithObject()
    {
        if (objectToInteract == null) return;
        objectToInteract.Interact();
    }
}
