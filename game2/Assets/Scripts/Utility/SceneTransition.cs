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
    [SerializeField] SceneTransitionManager.TransitionTags transitionTag;
    [SerializeField] Transform playerSpawnPos;
    [SerializeField] GameObject player;
    private void Start()
    {
        if(SceneTransitionManager.tagToTeleportPlayer == transitionTag)
        {
            player.transform.position = playerSpawnPos.position;
        }
    }

    public void Load()
    {
        SceneTransitionManager.tagToTeleportPlayer = transitionTag;
        SceneManager.LoadScene(sceneToLoad);
    }
}
