using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public string playerSceneName;
    public string lastSavedDate="";
    public int saveIndex=-1;
    public PlayerData playerData=null;
    public List<SceneData> sceneDatas = null;
    public List<ShortcutData> shortcutDatas = null;
    public SaveData(string date,int saveIndex,PlayerData playerData,List<SceneData> sceneDatas,List<ShortcutData> shortcutDatas,string playerSceneName)
    {
        lastSavedDate = date;
        this.saveIndex = saveIndex;
        this.playerData = playerData;
        this.sceneDatas = sceneDatas;
        this.shortcutDatas = shortcutDatas;
        this.playerSceneName = playerSceneName;
    }

    public SaveData(SaveData save)
    {
        lastSavedDate = save.lastSavedDate;
        saveIndex = save.saveIndex;
        playerData = new PlayerData(save.playerData);
        sceneDatas = new List<SceneData>();
        shortcutDatas = new List<ShortcutData>();
        playerSceneName = save.playerSceneName;
    }

    public SaveData(List<SceneState> sceneStates,List<ShortcutState> shortcutStates)
    {
        sceneDatas = new List<SceneData>();
        shortcutDatas=new List<ShortcutData>();
        foreach (SceneState sceneState in sceneStates)
        {
            List<bool> pickUpsStates = new List<bool>();
            List<bool> puzzlesStates = new List<bool>();
            sceneState.pickupStates.ForEach((state) => pickUpsStates.Add(state.value));
            sceneState.puzzleStates.ForEach((state) => puzzlesStates.Add(state.value));
            SceneData sceneData = new SceneData(pickUpsStates,puzzlesStates, sceneState.sceneEnum.scene);
            sceneDatas.Add(sceneData);
        }
        foreach (ShortcutState shortcutState in shortcutStates)
        {
            shortcutDatas.Add(new ShortcutData(shortcutState));
        }
    }
    public void CreateSceneDatas()
    {
        sceneDatas = new List<SceneData>();
    }
    public void AddSceneData(SceneData sceneData)
    {
        sceneDatas.Add(sceneData);
    }
    public void UnlockAbility(AbilityList.Abilities ability)
    {
        playerData.UnlockAbility(ability);
    }
}
