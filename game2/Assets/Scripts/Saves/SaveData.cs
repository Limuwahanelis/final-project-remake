using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SaveData
{
    public string lastSavedDate;
    public int saveIndex;
    public PlayerData playerData;

    public SaveData(string date,int saveIndex,PlayerData playerData)
    {
        lastSavedDate = date;
        this.saveIndex = saveIndex;
        this.playerData = playerData;
    }
}
