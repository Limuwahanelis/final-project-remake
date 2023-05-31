using Gamekit2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    [SceneName] public string sceneToLoad;
    [SerializeField] RectTransform _transitionCircleTransform;
    [SerializeField] SceneTransitionManager.TransitionTags _transitionTag;
    [SerializeField] Transform _playerSpawnPos;
    [SerializeField] GameObject _player;
    [SerializeField] Animator _anim;
    private void Start()
    {
        if(SceneTransitionManager.tagToTeleportPlayer == _transitionTag)
        {
            _anim.SetTrigger("FadeIn");
            _player.transform.position = _playerSpawnPos.position;
        }
    }

    public void Load()
    {
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
