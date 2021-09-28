using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{



    public BoolReference isGamePaused;
    public GameObject optionsPanel;
    public GameObject buttons;

    private void Start()
    {
        isGamePaused.value = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Unpause();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void Unpause()
    {
        optionsPanel.SetActive(false);
        buttons.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused.value = false;
        gameObject.SetActive(false);
    }

    public void Load()
    {

    }
}
