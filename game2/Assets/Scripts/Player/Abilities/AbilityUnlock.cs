using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class AbilityUnlock : MonoBehaviour,IInteractable
{
    public AbilityList abilityList;
    public AbilityList.Abilities ability;
    public float speed;
    public PlayableDirector timeline;
    public BoolReference isGamePaused;
    private PlayerInteract _playerInteract;
    private void Start()
    {
        //_pos = Camera.main.ScreenToWorldPoint(UIIconPos.transform.position);
    }
    private void Update()
    {
        //if(_mooveToUIPos)
        //{
        //    MoveToIconPos();
        //}
    }
    public void Interact()
    {
        abilityList.UnlockAbility(ability);
        isGamePaused.value = true;
        timeline.Play();
        Time.timeScale = 0f;
        //_mooveToUIPos = true;
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

    private void MoveToIconPos()
    {
        //if (Vector2.Distance(transform.position, _pos) > 0.1)
        //{
        //    Vector3 toMove = Vector3.MoveTowards(transform.position, _pos, speed * Time.deltaTime);
        //    transform.position = new Vector3(toMove.x, toMove.y);
        //}
        //else
        //{
        //    _mooveToUIPos = false;
        //    UIPanelToMove.gameObject.SetActive(true);
        //    UIPanelToMove.PlayAnimation();
        //}

    }

    private void ActivateSequence()
    {

    }
}
