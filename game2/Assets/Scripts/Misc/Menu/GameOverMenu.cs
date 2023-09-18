using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] GameObject _darkPanel;
    [SerializeField] GameObject _gameOverScreen;
    public void ShowGameOverScreen()
    {
        _darkPanel.SetActive(true);
        _gameOverScreen.SetActive(true);
    }
}
