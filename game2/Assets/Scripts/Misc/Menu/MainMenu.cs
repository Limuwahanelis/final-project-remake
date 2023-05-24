using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SceneName] public string firstScene;
    public void Play()
    {
        SceneManager.LoadScene(firstScene);
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
}
