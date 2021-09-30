using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSaver : MonoBehaviour
{

    public FloatReference globalAudioVolume;
    [SerializeField]
    private Settings settings;

    public void SaveSettings()
    {
        SaveSystem.SaveConfigs(globalAudioVolume.value, settings.currentResolution, settings.fullScreen);
    }
}
