using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour, IInteractable
{
    public System.Action<PickUp> OnPickUp;
    public string pickUpMessage;
    protected PlayerInteract _playerInteract;
    public GameObject canvas;
    public bool pickedUp;

    public virtual void Interact()
    {
        OnPickUp?.Invoke(this);
    }
}
