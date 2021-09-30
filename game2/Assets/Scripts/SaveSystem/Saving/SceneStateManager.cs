using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    [SerializeField]
    private PickUpsManager pickUpsManager;

    private void Start()
    {
        if (SaveSystem.tmpSave.scene1Data == null)
        {
            List<bool> values = new List<bool>();
            for (int i = 0; i < pickUpsManager.pickUpsInScene.Count; i++)
            {
                values.Add(pickUpsManager.pickUpsInScene[i].pickedUp);
            }
            SceneData sceneData = new SceneData(values);
            SaveSystem.tmpSave.AddSceneData(sceneData);
        }
        else
        {
            pickUpsManager.DestroyPickedPickUps(SaveSystem.tmpSave.scene1Data.wasPickUpPicked);
        }
    }

    public void ChangePickUpState(int index,bool value)
    {
        SaveSystem.tmpSave.scene1Data.wasPickUpPicked[index] = value;
    }

}
