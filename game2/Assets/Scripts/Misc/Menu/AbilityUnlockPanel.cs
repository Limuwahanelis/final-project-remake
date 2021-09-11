using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AbilityUnlockPanel : MonoBehaviour
{
    private Vector3 _startingPos;
    public GameObject pausePanel;
    public BoolValue isGamePaused;
    public Image abilityIcon;
    public TMP_Text abilityDescription;
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

    public void ChangeAbiltyToShow(Sprite abilitySprite, string abilityText)
    {
        abilityIcon.sprite = abilitySprite;
        abilityDescription.text = abilityText;
    }


}
