using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{
    public List<SaveButton> saves;
    public IntReference saveIndexToLoad;
    public BoolReference loadSave;
    public BoolReference isGamePaused;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private PlayerHealthSystem _playerHealthSystem;
    [SerializeField]
    private GameObject _darkPanel;
    public void LoadSave(SaveButton save)
    {
        if (SaveSystem.CheckIfSaveFileExists(save.saveIndex))
        {

            loadSave.value = true;
            saveIndexToLoad.value = save.saveIndex;
            SceneManager.LoadScene(1);
        }
    }

    public void GetSaveFiles()
    {
        for(int i=0;i<saves.Count;i++)
        {
            SaveData save = SaveSystem.GetSave(i);
            if(save!=null)
            {
                SaveButton button = saves.Find((x) => x.saveIndex == i);
                if(button!=null) button.SetDescription(save.lastSavedDate);
                else button.SetDescription("");
            }
        }
    }

    public void SaveGame(SaveButton pressedSaveButton)
    {
        SaveSystem.SaveGame(_player, _playerHealthSystem,pressedSaveButton.saveIndex);
        isGamePaused.value = false;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        
    }
}
