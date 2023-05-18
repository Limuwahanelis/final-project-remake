using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public List<bool> wasPickUpPicked=new List<bool>();
    public List<bool> wasPuzzleSolved=new List<bool>();
    public bool wasShortcutUnlocked;
    public SceneData(List<bool> pickUpValues, List<bool>puzzlesValues,bool isShortcutUnlocked)
    {
        for(int i=0;i< pickUpValues.Count;i++)
        {
            wasPickUpPicked.Add(pickUpValues[i]);
        }
        foreach(bool puzzleSol in puzzlesValues)
        {
            wasPuzzleSolved.Add(puzzleSol);
        }
        wasShortcutUnlocked = isShortcutUnlocked;
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
        wasShortcutUnlocked = sceneData.wasShortcutUnlocked;
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
