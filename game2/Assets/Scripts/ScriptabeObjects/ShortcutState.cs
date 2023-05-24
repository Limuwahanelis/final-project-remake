using Gamekit2D;
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

    private bool isInitalised = false;
    private void Awake()
    {
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
