using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public string lastSavedDate="";
    public int saveIndex=-1;
    public PlayerData playerData=null;
    public SceneData scene1Data=null;
    public SaveData(string date,int saveIndex,PlayerData playerData,SceneData sceneData)
    {
        lastSavedDate = date;
        this.saveIndex = saveIndex;
        this.playerData = playerData;
        scene1Data = sceneData;
        //wasPickUpPicked = pickUps;
    }

    public SaveData(SaveData save)
    {
        lastSavedDate = save.lastSavedDate;
        saveIndex = save.saveIndex;
        playerData = new PlayerData(save.playerData);
        scene1Data = new SceneData();
    }

    public SaveData()
    {

    }

    public void AddSceneData(SceneData sceneData)
    {
        scene1Data = sceneData;
    }
}
