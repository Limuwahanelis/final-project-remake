using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenSetUp : MonoBehaviour
{
    [SceneName] public string mainmenuScene;
    private Resolution[] allResolutions;
    private List<Resolution> availableResolutions = new List<Resolution>();
    private void Awake()
    {
        if (SaveSystem.GetConfigs() == null)
        {
            GetAllResolutions();
            SaveSystem.SaveConfigs(0.5f, new Settings.MyResolution(availableResolutions[availableResolutions.Count-1]), true);


        }
    }
    void GetAllResolutions()
    {
        PlayerConfigsData configs = SaveSystem.GetConfigs();
        allResolutions = Screen.resolutions;
        availableResolutions = allResolutions.ToList().FindAll(x => x.refreshRate == Screen.currentResolution.refreshRate);
        for (int i = 0; i < availableResolutions.Count; i++)
        {
            availableResolutions.Sort((r1, r2) => r1.height.CompareTo(r2.height));
            availableResolutions.Sort((r1, r2) => r1.width.CompareTo(r2.width));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
