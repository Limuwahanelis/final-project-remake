using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCreator : MonoBehaviour
{
    [SerializeField] List<SceneState> sceneDataList = new List<SceneState>();
    [SerializeField] List<ShortcutState> shortcutDataList = new List<ShortcutState>();
#if UNITY_EDITOR
    [SerializeField] bool createSaveOnStart;
#endif
    public void CreateTmpSave()
    {
        Debug.Log("dasdsadsaas: "+shortcutDataList[0].Id);
        SaveSystem.CreateTmpSave(sceneDataList, shortcutDataList);
    }

#if UNITY_EDITOR

    private void Awake()
    {
        if(createSaveOnStart) CreateTmpSave();

    }

    private void OnValidate()
    {
        int i = 0;
        foreach(SceneState s in sceneDataList)
        {
            s.sceneEnum.sceneNum = i;
            i++;
        }
    }
#endif
}
