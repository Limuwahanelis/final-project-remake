using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] BoolReference isGamePaused;
    [SerializeField] GameObject darkPanel;
    [SerializeField] GameObject pausePanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            SetPause(!isGamePaused.value);
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void SetPause(bool value)
    {
        isGamePaused.value = value;
        Time.timeScale = isGamePaused.value ? 0f:1f;
        pausePanel.SetActive(isGamePaused.value);
        darkPanel.SetActive(isGamePaused.value);
       
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
