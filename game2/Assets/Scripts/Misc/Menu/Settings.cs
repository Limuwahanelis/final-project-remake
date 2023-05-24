using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
public class Settings : MonoBehaviour
{
    struct ResIndex
    {
        public int refreshRateIndex;
        public int resolutionIndex;
    }
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown refreshRateDropdown;
    public Toggle fullScreenToggle;
    public Resolution currentResolution;
    public bool fullScreen;
    Resolution[] allResolutions;
    ResIndex currentResIndex;
    List<List<Resolution>> resolutions = new List<List<Resolution>>();
    List<int> refreshRates = new List<int>();
    List<string> refreshRatesS = new List<string>();
    List<List<string>> resolutionOptions = new List<List<string>>();

    // Start is called before the first frame update

    private void Awake()
    {

    }
    void Start()
    {
        currentResIndex.refreshRateIndex = 0;
        currentResIndex.resolutionIndex = 0;

        allResolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        refreshRateDropdown.ClearOptions();

        FindAllRefreshRates();

        AddResolutionsToLists();

        
        for (int i = 0; i < resolutions.Count; i++)
        {
            resolutions[i].Sort((r1, r2) => r1.height.CompareTo(r2.height));
            resolutions[i].Sort((r1, r2) => r1.width.CompareTo(r2.width));
        }
        refreshRateDropdown.AddOptions(refreshRatesS);
        refreshRateDropdown.RefreshShownValue();
        resolutionDropdown.AddOptions(resolutionOptions[0]);

        SetResolution(resolutionDropdown.options.Count - 1);
        SelectRefreshRate(refreshRateDropdown.options.Count - 1);
        SetFullScreen(true);

    }
    private void AddResolutionsToLists()
    {
        for (int i = 0; i < refreshRates.Count; i++)
        {
            resolutions.Add(new List<Resolution>());
            resolutionOptions.Add(new List<string>());
        }

        for (int i = 0; i < allResolutions.Length; i++)
        {
            int refreshRateIndex = -1;

            for (int k = 0; k < refreshRates.Count; k++)
            {
                if (refreshRates[k] == allResolutions[i].refreshRate)
                {
                    refreshRateIndex = k;
                    break;
                }

            }
            resolutions[refreshRateIndex].Add(allResolutions[i]);

        }

        RemoveNonReapetingResolutions();
    }

    private void RemoveNonReapetingResolutions()
    {

        List<Resolution> reapitingResolutions = new List<Resolution> (resolutions[0]);
        List<Resolution> toRemove=new List<Resolution>();
        Resolution toCheckRes = new Resolution();
        for (int i=1;i<resolutions.Count;i++)
        {
            toRemove.Clear();
            toCheckRes.refreshRate = refreshRates[i];
            for (int j=0;j< reapitingResolutions.Count;j++)
            {
                toCheckRes.width = reapitingResolutions[i].width;
                toCheckRes.height = reapitingResolutions[i].height;
                if (!resolutions[i].Contains(toCheckRes)) toRemove.Add(reapitingResolutions[j]);
            }
            reapitingResolutions.RemoveAll(x=>toRemove.Contains(x));
        }

        for (int i=0;i<refreshRates.Count;i++)
        {
            resolutions[i].Clear();
            List<Resolution> tmp = new List<Resolution>();
            for(int j=0;j<reapitingResolutions.Count;j++)
            {
                Resolution newRes=new Resolution();
                newRes.width = reapitingResolutions[j].width;
                newRes.height = reapitingResolutions[j].height;
                newRes.refreshRate = refreshRates[i];
                tmp.Add(newRes);
                resolutionOptions[i].Add(newRes.width + " x " + newRes.height);
            }
           
            resolutions[i] = tmp;
        }

    }
    private void FindAllRefreshRates()
    {
        for (int i = 0; i < allResolutions.Length; i++)
        {
            if (!refreshRates.Exists((x) => x == allResolutions[i].refreshRate))
            {
                refreshRates.Add(allResolutions[i].refreshRate);
            }
        }
        refreshRates.Sort();
        for(int i=0;i<refreshRates.Count;i++)
        {
            refreshRatesS.Add(refreshRates[i].ToString() + " Hz");
        }
        
    }
    public void SetResolution(int resolutionIndex)
    {
        currentResIndex.resolutionIndex = resolutionIndex;
        currentResolution = resolutions[currentResIndex.refreshRateIndex][currentResIndex.resolutionIndex];
        Screen.SetResolution(resolutions[currentResIndex.refreshRateIndex][currentResIndex.resolutionIndex].width, resolutions[currentResIndex.refreshRateIndex][currentResIndex.resolutionIndex].height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        fullScreenToggle.isOn = isFullscreen;
        fullScreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void SelectRefreshRate(int refreshRateIndex)
    {
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionOptions[refreshRateIndex]);
        resolutionDropdown.RefreshShownValue();
        currentResIndex.refreshRateIndex = refreshRateIndex;
        Resolution res = new Resolution();

        res.width = resolutions[currentResIndex.refreshRateIndex][currentResIndex.resolutionIndex].width;
        res.height = resolutions[currentResIndex.refreshRateIndex][currentResIndex.resolutionIndex].height;
        res.refreshRate = currentResIndex.refreshRateIndex;

        resolutionDropdown.value = currentResIndex.resolutionIndex;
        refreshRateDropdown.value=currentResIndex.refreshRateIndex;

        Screen.SetResolution(res.width, res.height, fullScreen, refreshRates[currentResIndex.refreshRateIndex]);

    }
    public Resolution GetResolution()
    {
        return resolutions[currentResIndex.refreshRateIndex][currentResIndex.resolutionIndex];
    }

    public bool GetFullScreen()
    {
        return fullScreen;
    }
}
