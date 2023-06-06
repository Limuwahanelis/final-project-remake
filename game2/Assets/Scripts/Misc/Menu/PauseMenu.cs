using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] BoolReference isGamePaused;
    [SerializeField] GameObject darkPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject savePanel;
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{ 
        //    SetPause(!isGamePaused.value);
        //    SetPausePanel(isGamePaused.value);
        //}
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void SetPause(bool value)
    {
        isGamePaused.value = value;
        Time.timeScale = isGamePaused.value ? 0f:1f;
        darkPanel.SetActive(isGamePaused.value);
        if(!isGamePaused.value)
        {
            SetSavePanel(false);
            SetPausePanel(false);
        }
    }
    public void SetPausePanel(bool value)
    {
        pausePanel.SetActive(value);
    }
    public void SetSavePanel(bool value)
    {
        savePanel.SetActive(value);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
