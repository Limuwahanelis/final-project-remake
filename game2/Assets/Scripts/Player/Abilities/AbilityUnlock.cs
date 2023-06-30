using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class AbilityUnlock : MonoBehaviour,IInteractable
{
    public AbilityList abilityList;
    public AbilityList.Abilities ability;
    public PlayableDirector timeline;
    public BoolReference isGamePaused;
    private PlayerInteract _playerInteract;
    public AbilityUnlockPanel panel;
    public string abilityDescription;
    public TimelineAsset cutscene;
    [SerializeField] GameObject _canvas;
    public void Interact()
    {
        abilityList.UnlockAbility(ability);
        SaveSystem.UnlockAblity(ability);
        panel.ChangeAbiltyToShow(GetComponentInChildren<SpriteRenderer>().sprite, abilityDescription);
        isGamePaused.value = true;
        _canvas.SetActive(false);
        timeline.Play(cutscene);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("fsaf");
        _playerInteract = collision.GetComponentInParent<PlayerInteract>();
        _playerInteract.setObjectToInteract(this);
        _canvas.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerInteract.RemoveObjectToInteract();
        _playerInteract = null;
        _canvas.SetActive(false);
    }
}
