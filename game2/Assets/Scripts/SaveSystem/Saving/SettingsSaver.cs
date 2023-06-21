using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSaver : MonoBehaviour
{

    public FloatReference globalAudioVolume;
    [SerializeField]
    private Settings settings;

    private void Awake()
    {
        if(SaveSystem.GetConfigs()==null)
        {
            Debug.Log("no configs");
            //settings.SetDefaultOptions();
            globalAudioVolume.value = 0.5f;
            SaveSystem.SaveConfigs(globalAudioVolume.value, new Settings.MyResolution(Screen.currentResolution), true);


        }
        
    }
    public void SaveSettings()
    {
        Debug.Log($"save fullscren is {settings.fullScreen}");
        SaveSystem.SaveConfigs(globalAudioVolume.value, settings.selectedResolution, settings.fullScreen);
    }
    
}
