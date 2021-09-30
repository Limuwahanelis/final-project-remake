using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerConfigsData
{
    public float globalVolume;
    public Resolution resolution;
    public bool fullScreen;
    public PlayerConfigsData(float globalVolume,Resolution resolution,bool fullScreen)
    {
        this.globalVolume = globalVolume;
        this.resolution = resolution;
        this.fullScreen = fullScreen;
    }
}
