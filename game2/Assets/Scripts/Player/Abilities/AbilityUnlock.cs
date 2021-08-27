using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUnlock : MonoBehaviour,IInteractable
{
    public AbilityList abilityList;
    public AbilityList.Abilities ability;
    private PlayerInteract _playerInteract;
    public void Interact()
    {
        abilityList.UnlockAbility(ability);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("fsaf");
        _playerInteract = collision.GetComponentInParent<PlayerInteract>();
        _playerInteract.setObjectToInteract(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerInteract.RemoveObjectToInteract();
        _playerInteract = null;
    }

}
