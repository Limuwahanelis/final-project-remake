using Gamekit2D;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            //Screen.SetResolution(allResolutions[0].width, availableResolutions[0].height, true);
            Debug.Log("org res: " + availableResolutions[availableResolutions.Count - 1]);
            SaveSystem.SaveConfigs(0.5f, new Settings.MyResolution(availableResolutions[availableResolutions.Count-1]), true);
            Screen.SetResolution(availableResolutions[availableResolutions.Count - 1].width, availableResolutions[availableResolutions.Count - 1].height, Screen.fullScreen);
        }
        SceneManager.LoadScene(mainmenuScene);
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
