using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class GlobalAudioManager : MonoBehaviour
{
    public static Action<float> OnGlobalVolumeChange;
    public static GlobalAudioManager instance=null;
    [Range(0,1)]
    public float globalVolume;
    void Awake()
    {
        DontDestroyOnLoad(this);
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(gameObject);
        }
    }
    public void SetGlobalVolume(float value)
    {
        globalVolume = value;
        OnGlobalVolumeChange?.Invoke(value);
    }
}
