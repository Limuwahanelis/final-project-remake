using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIncreasePickUp : MonoBehaviour, IInteractable
{
    public int attackIncrease;
    public GameObject canvas;
    public IntReference playerDamage;
    public string pickUpMessage;
    private PlayerInteract _playerInteract;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Interact()
    {
        // gameMan.GetPlayer().GetComponent<PlayerCombat>().IncraseAttackDamage(attackIncrease);
        //gameMan.SetMessage(pickUpMessage);
        playerDamage.value += attackIncrease;
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
