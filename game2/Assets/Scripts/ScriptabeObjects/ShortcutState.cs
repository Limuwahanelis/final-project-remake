using Gamekit2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName ="Scene Shortcut")]
public class ShortcutState : ScriptableObject
{
    [SceneName] public string Scene1;
    [SceneName] public string Scene2;
    [SerializeField] public string Id { get { return id; } private set { id = value; } }
    [SerializeField] private string id;
    [SerializeField] public bool IsUnlocked;

    [SerializeField] public bool IsInitalised { get { return isInitalised; } private set { isInitalised = value; } }
    [SerializeField] private bool isInitalised;
    private void Awake()
    {
        Debug.Log(IsInitalised);
        Debug.Log("My ID is: " + Id);
#if UNITY_EDITOR

        if (!isInitalised)
        {
            Id = GUID.Generate().ToString();
            isInitalised = true;
        }
#endif
    }

}
