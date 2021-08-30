using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ann
{
    public string name;
    public float duration;

    public Ann(string name, float duration)
    {
        this.name = name;
        this.duration = duration;
    }
}
