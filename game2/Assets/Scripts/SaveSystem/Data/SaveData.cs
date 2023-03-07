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
    public List<SceneData> sceneDatas = null;
    public SaveData(string date,int saveIndex,PlayerData playerData,List<SceneData> sceneDatas)
    {
        lastSavedDate = date;
        this.saveIndex = saveIndex;
        this.playerData = playerData;
        this.sceneDatas = sceneDatas;

    }

    public SaveData(SaveData save)
    {
        lastSavedDate = save.lastSavedDate;
        saveIndex = save.saveIndex;
        playerData = new PlayerData(save.playerData);
        sceneDatas = new List<SceneData>();
    }

    public SaveData()
    {
    }
    public void CreateSceneDatas()
    {
        sceneDatas = new List<SceneData>();
    }
    public void AddSceneData(SceneData sceneData)
    {
        sceneDatas.Add(sceneData);
    }
}
