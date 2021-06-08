﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{



    static public MainMenu menu;
    public GameObject controlPanel;
    public GameObject optionsPanel;
    public GameObject buttons;
    

    public void Awake()
    {
        menu = this;
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Back(GameObject panel)
    {
        panel.SetActive(false);
        buttons.SetActive(true);
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void Controls()
    {
        controlPanel.SetActive(true);
        buttons.SetActive(false);
    }
    public void Options()
    {
        optionsPanel.SetActive(true);
        buttons.SetActive(false);
    }
}
