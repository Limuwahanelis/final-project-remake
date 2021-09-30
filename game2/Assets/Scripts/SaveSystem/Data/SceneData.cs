using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public List<bool> wasPickUpPicked=new List<bool>();

    public SceneData(List<bool> values)
    {
        for(int i=0;i<values.Count;i++)
        {
            wasPickUpPicked.Add(values[i]);
        }
    }
    public SceneData(SceneData sceneData)
    {
        for (int i = 0; i < sceneData.wasPickUpPicked.Count; i++)
        {
            wasPickUpPicked.Add(sceneData.wasPickUpPicked[i]);
        }
    }

    public SceneData()
    {
        wasPickUpPicked = new List<bool>();
    }

    public void ChangePickUpSate(int index, bool value)
    {
        wasPickUpPicked[index] = true;
    }
}
