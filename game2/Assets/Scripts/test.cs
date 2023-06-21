using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class test : MonoBehaviour
{
    [System.Serializable]
    public struct MyResolution
    {
        public int width;
        public int height;
        public int refreshRate;
    }
    public void Start()
    {
        MyResolution resolution = new MyResolution
        {
            width = 5,
            height = 6,
            refreshRate = 20
        };
        string json = JsonUtility.ToJson(resolution);
            File.WriteAllText(Application.dataPath+@"/aa.json", json);
    }
}
