using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public List<bool> wasPickUpPicked=new List<bool>();
    public List<bool> wasPuzzleSolved=new List<bool>();
    public List<bool> wasAbilityUnlocked =new List<bool>();
    public string sceneName;
    public SceneData(List<bool> pickUpValues, List<bool>puzzlesValues, string sceneName)
    {
        for(int i=0;i< pickUpValues.Count;i++)
        {
            wasPickUpPicked.Add(pickUpValues[i]);
        }
        foreach(bool puzzleSol in puzzlesValues)
        {
            wasPuzzleSolved.Add(puzzleSol);
        }
        this.sceneName = sceneName;
    }
    public SceneData(SceneData sceneData, string sceneName)
    {
        for (int i = 0; i < sceneData.wasPickUpPicked.Count; i++)
        {
            wasPickUpPicked.Add(sceneData.wasPickUpPicked[i]);
        }
        foreach (bool puzzleSol in sceneData.wasPuzzleSolved)
        {
            wasPuzzleSolved.Add(puzzleSol);
        }
        this.sceneName = sceneName;
    }

    public SceneData()
    {
        wasPickUpPicked = new List<bool>();
        wasPuzzleSolved = new List<bool>();
    }

    public void ChangePickUpSate(int index, bool value)
    {
        wasPickUpPicked[index] = true;
    }
}
