using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxHPincreasePickUp : MonoBehaviour,IInteractable
{
    //private GameManager gameMan;
    public int maxHPincrease;
    public GameObject canvas;
    public IntReference playerMaxHealth;
    public string pickUpMessage;
    private PlayerInteract _playerInteract;
    public void Interact()
    {
        playerMaxHealth.value += maxHPincrease;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas.SetActive(true);
        _playerInteract = collision.GetComponentInParent<PlayerInteract>();
        _playerInteract.setObjectToInteract(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvas.SetActive(false);
        _playerInteract.RemoveObjectToInteract();
        _playerInteract = null;
    }
}
