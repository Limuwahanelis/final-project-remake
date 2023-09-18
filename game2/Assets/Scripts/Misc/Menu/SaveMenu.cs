using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveMenu : MonoBehaviour
{
    public List<SaveButton> saves;
    public BoolReference loadSave;
    public BoolReference isGamePaused;
    [SerializeField]
    private GameObject _darkPanel;
    [SerializeField]
    private SaveManager _saveManager;

    private void Start()
    {
        DescribeSaveButtons();
    }
    public void LoadSave(SaveButton save)
    {
        if (SaveSystem.CheckIfSaveFileExists(save.saveIndex))
        {
            SaveSystem.SetSave(save.saveIndex);
            loadSave.value = true;
            SceneManager.LoadScene(SaveSystem.tmpSave.playerSceneName);
        }
    }

    public void DescribeSaveButtons()
    {
        for(int i=0;i<saves.Count;i++)
        {
            SaveData save = SaveSystem.GetSaveFile(i);
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
        _saveManager.Save(pressedSaveButton.saveIndex);
        isGamePaused.value = false;
        Time.timeScale = 1f;
        _darkPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        
    }
}
