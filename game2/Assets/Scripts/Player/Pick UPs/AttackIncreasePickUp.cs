using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIncreasePickUp : PickUp
{
    public int attackIncrease;
    public IntReference playerDamage;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void Interact()
    {
        base.Interact();
        // gameMan.GetPlayer().GetComponent<PlayerCombat>().IncraseAttackDamage(attackIncrease);
        //gameMan.SetMessage(pickUpMessage);
        playerDamage.value += attackIncrease;
        pickedUp.value = true;
        //gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.activeSelf)
        {
            canvas.SetActive(true);
            _playerInteract = collision.GetComponentInParent<PlayerInteract>();
            _playerInteract.setObjectToInteract(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.activeSelf)
        {
            canvas.SetActive(false);
            _playerInteract.RemoveObjectToInteract();
            _playerInteract = null;
        }
    }
}
