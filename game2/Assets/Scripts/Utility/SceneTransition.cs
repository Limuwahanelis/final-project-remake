using Gamekit2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    [SceneName] public string sceneToLoad;
    [SerializeField] RectTransform _transitionCircleTransform;
    [SerializeField] SceneTransitionManager.TransitionTags _transitionTag;
    [SerializeField] Transform _playerSpawnPos;
    [SerializeField] GameObject _player;
    [SerializeField] Animator _anim;
    [SerializeField] InputActionAsset _playerControls;
    [SerializeField] InputActionAsset _menuControls;
    private void Start()
    {
        if(SceneTransitionManager.tagToTeleportPlayer == _transitionTag)
        {
            _anim.SetTrigger("FadeIn");
            _player.transform.position = _playerSpawnPos.position;
            _playerControls.Enable();
            _menuControls.Enable();
        }
    }

    public void Load()
    {
        _playerControls.Disable();
        _menuControls.Disable();
        StartCoroutine(TransitionCor());
    }
    IEnumerator TransitionCor()
    {
        _anim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.7f);
        SceneTransitionManager.tagToTeleportPlayer = _transitionTag;
        SceneManager.LoadScene(sceneToLoad);
    }
}
