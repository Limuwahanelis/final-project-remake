using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SceneStateManager : MonoBehaviour
{

    public UnityEvent UnlockShortcut;
    [SerializeField] private PickUpsManager pickUpsManager;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private int sceneNum;
    [SerializeField] ShortcutState shortcutState;
    private void Start()
    {
        if (SaveSystem.tmpSave.shortcutDatas.Find(x=>x.Id==shortcutState.Id).IsUnlocked) UnlockShortcut?.Invoke();
            pickUpsManager.DestroyPickedPickUps(SaveSystem.tmpSave.sceneDatas[sceneNum].wasPickUpPicked);
            puzzleManager.MarkPuzzlesAsSolved(SaveSystem.tmpSave.sceneDatas[sceneNum].wasPuzzleSolved);
    }

    public void ChangePickUpState(int index,bool value)
    {
        SaveSystem.tmpSave.sceneDatas[sceneNum].wasPickUpPicked[index] = value;
    }
    public void ChangePuzzleState(int index,bool value)
    {
        SaveSystem.tmpSave.sceneDatas[sceneNum].wasPuzzleSolved[index] = value;
       
    }

    // used by unity event by destructable shortcuts in scene 2
    public void ChangeShortcutStateToUnlocked()
    {
        SaveSystem.tmpSave.shortcutDatas.Find(x => x.Id == shortcutState.Id).IsUnlocked = true;
    }
}
