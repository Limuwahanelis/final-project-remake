using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlace : MonoBehaviour,IInteractable
{
    [SerializeField]
    private SaveMenu _saveMenu;
    public BoolReference isGamePaused;
    public void Interact()
    {
        //SaveSystem.SavePlayerData(_player, _playerHealthSystem);
        Time.timeScale = 0f;
        isGamePaused.value = true;
        _saveMenu.gameObject.SetActive(true);
        _saveMenu.DescribeSaveButtons();
        Debug.Log("saved");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponentInParent<PlayerInteract>().setObjectToInteract(this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponentInParent<PlayerInteract>().RemoveObjectToInteract();
    }
}
