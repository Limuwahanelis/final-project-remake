using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlace : MonoBehaviour,IInteractable
{
    [SerializeField] private SaveMenu _saveMenu;
    [SerializeField] private PauseMenu _pauseMenu;
    public BoolReference isGamePaused;
    public void Interact()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetPause(true);
        _pauseMenu.SetSavePanel(true);
        _saveMenu.DescribeSaveButtons();
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
