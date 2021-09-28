using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public static class SaveSystem
{
    public static int numberOfSaveSlots = 6;
    public static string saveFolderPath = Application.dataPath + @"\saves";
    //public static PlayerData SavePlayerData(Player player,PlayerHealthSystem playerHealthSystem)
    //{
    //    PlayerData playerData = new PlayerData(player, playerHealthSystem, player.abilities);
    //    string json = JsonUtility.ToJson(playerData);
    //    //if(!Directory.Exists(playerDataPath))
    //    //{
    //    //    Directory.CreateDirectory(playerDataPath);
    //    //}
    //    //File.WriteAllText(playerDataPath + @"\player.json", json);
    //}

    public static PlayerData LoadPlayerData(int saveIndex)
    {
        SaveData saveData = GetSave(saveIndex);
        return saveData.playerData;
    }

    public static void SaveGame(Player player, PlayerHealthSystem playerHealthSystem,int saveIndex)
    {
        PlayerData playerData = new PlayerData(player, playerHealthSystem, player.abilities);
        string today = DateTime.Today.ToString("dd/MM/yyyy");
        SaveData saveData = new SaveData(today, saveIndex, playerData);

        string json = JsonUtility.ToJson(saveData);
        Debug.Log(json);
        Debug.Log(playerHealthSystem.currentHP.value);
        if (!Directory.Exists(saveFolderPath))
        {
            Directory.CreateDirectory(saveFolderPath);
        }

        string filePath = saveFolderPath + @"\saveData" + saveIndex + ".json";
        File.WriteAllText(filePath, json);
    }

    public static SaveData GetSave(int saveIndex)
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
    public static bool CheckIfSaveFileExists(int saveIndex)
    {
        string filePath = saveFolderPath + @"\saveData" + saveIndex + ".json";
        if (File.Exists(filePath))
        {
            return true;
        }
        return false;
    }
}
