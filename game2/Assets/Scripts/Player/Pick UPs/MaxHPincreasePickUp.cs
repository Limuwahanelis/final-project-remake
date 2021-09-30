using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaxHPincreasePickUp : PickUp
{
    //private GameManager gameMan;
    public int maxHPincrease;
    public IntReference playerMaxHealth;
    public override void Interact()
    {
        base.Interact();
        playerMaxHealth.value += maxHPincrease;
        _playerInteract.GetComponentInParent<PlayerHealthSystem>().IncreaseHealthBarMaxValue();
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
