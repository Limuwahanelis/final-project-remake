using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Scene Enum")]
public class SceneEnum : ScriptableObject
{
    [SceneName] public string scene;
    internal int sceneNum=0;
}
