using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUnlockPanel : MonoBehaviour
{
    private Vector3 _startingPos;
    public GameObject pausePanel;
    public BoolValue isGamePaused;
    private void Start()
    {
        _startingPos = gameObject.GetComponent<RectTransform>().position;
    }
    public void ResumeGame()
    {
        
        Time.timeScale = 1f;
        isGamePaused.value = false;
        gameObject.GetComponent<RectTransform>().position = _startingPos;
        gameObject.SetActive(false);
        pausePanel.SetActive(false);
    }


}
