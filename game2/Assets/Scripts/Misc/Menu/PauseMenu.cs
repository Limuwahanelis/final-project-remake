using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] BoolReference _isGamePaused;
    [SerializeField] GameObject _darkPanel;
    [SerializeField] GameObject _pausePanel;
    [SerializeField] GameObject _savePanel;
    [SerializeField] InputActionReference _menuAction;
    private void Awake()
    {
        _menuAction.action.started += Pause;
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{ 
        //    SetPause(!_isGamePaused.value);
        //    SetPausePanel(_isGamePaused.value);
        //}
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void SetPause(bool value)
    {
        _isGamePaused.value = value;
        Time.timeScale = _isGamePaused.value ? 0f:1f;
        _darkPanel.SetActive(_isGamePaused.value);
        if(!_isGamePaused.value)
        {
            SetSavePanel(false);
            SetPausePanel(false);
        }
    }
    public void SetPausePanel(bool value)
    {
        _pausePanel.SetActive(value);
    }
    public void SetSavePanel(bool value)
    {
        _savePanel.SetActive(value);
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
    private void Pause(InputAction.CallbackContext context)
    {

        SetPause(!_isGamePaused.value);
        SetPausePanel(_isGamePaused.value);
    }

    private void OnDestroy()
    {
        _menuAction.action.started -= Pause;
    }
}
