using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public List<bool> wasPickUpPicked=new List<bool>();
    public List<bool> wasPuzzleSolved=new List<bool>();
    public List<bool> wasAbilityUnlocked =new List<bool>();
    public SceneData(List<bool> pickUpValues, List<bool>puzzlesValues)
    {
        for(int i=0;i< pickUpValues.Count;i++)
        {
            wasPickUpPicked.Add(pickUpValues[i]);
        }
        foreach(bool puzzleSol in puzzlesValues)
        {
            wasPuzzleSolved.Add(puzzleSol);
        }
    }
    public SceneData(SceneData sceneData)
    {
        for (int i = 0; i < sceneData.wasPickUpPicked.Count; i++)
        {
            wasPickUpPicked.Add(sceneData.wasPickUpPicked[i]);
        }
        foreach (bool puzzleSol in sceneData.wasPuzzleSolved)
        {
            wasPuzzleSolved.Add(puzzleSol);
        }
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
