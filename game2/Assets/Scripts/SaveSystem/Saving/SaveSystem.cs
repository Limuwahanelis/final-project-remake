using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public static class SaveSystem
{
    public static int numberOfSaveSlots = 6;
    public static string saveFolderPath = Application.dataPath + @"\saves";
    public static string configsFolderPath = Application.dataPath + @"\configs";
    public static string configsFilePath= configsFolderPath + @"\configs.json";
    public static SaveData tmpSave;

    public static PlayerData LoadPlayerData()
    {
        return tmpSave.playerData;
    }

    public static void CreateTmpSave(List<SceneState> sceneStates,List<ShortcutState> shortcutStates)
    {
        tmpSave = new SaveData(sceneStates,shortcutStates);
    }

    public static void SaveGame(Player player, PlayerHealthSystem playerHealthSystem,int saveIndex)
    {
        PlayerData playerData = new PlayerData(player, playerHealthSystem, player.abilities);
        string today = DateTime.Today.ToString("dd/MM/yyyy");
        SaveData saveData = new SaveData(today, saveIndex, playerData,tmpSave.sceneDatas,tmpSave.shortcutDatas);
        tmpSave = saveData;
        string json = JsonUtility.ToJson(saveData);
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        string filePath = saveFolderPath + @"\saveData" + saveIndex + ".json";
        File.WriteAllText(filePath, json);
    }

    public static SaveData GetSaveFile(int saveIndex)
    {
        string filePath = saveFolderPath + @"\saveData" + saveIndex + ".json";
        string json;
        SaveData saveData = null ;
        if (File.Exists(filePath))
        {
            json = File.ReadAllText(filePath);
            saveData = JsonUtility.FromJson<SaveData>(json);

        }
        return saveData;
    }

    public static void SetSave(int saveIndex)
    {
        tmpSave = GetSaveFile(saveIndex);
    }
    public static bool CheckIfSaveFileExists(int saveIndex)
    {
        string filePath = saveFolderPath + @"\saveData" + saveIndex + ".json";
        if (File.Exists(filePath))
        {
            return true;
        }
        return false;
    }

    public static void SaveConfigs(float globalVolume, Resolution resolution,bool fullScreen)
    {
        PlayerConfigsData configs = new PlayerConfigsData(globalVolume, resolution, fullScreen);
        string json = JsonUtility.ToJson(configs);

        if (!Directory.Exists(configsFolderPath))
        {
            Directory.CreateDirectory(configsFolderPath);
        }
        File.WriteAllText(configsFilePath, json);
    }

    public static PlayerConfigsData GetConfigs()
    {
        PlayerConfigsData configs = null;
        string json;
        if (File.Exists(configsFilePath))
        {
            json = File.ReadAllText(configsFilePath);
            configs = JsonUtility.FromJson<PlayerConfigsData>(json);
        }

        return configs;
    }


}
