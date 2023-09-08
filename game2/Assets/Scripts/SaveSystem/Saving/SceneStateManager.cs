using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class SceneStateManager : MonoBehaviour
{

    public UnityEvent UnlockShortcut;
    [SerializeField] PickUpsManager pickUpsManager;
    [SerializeField] PuzzleManager puzzleManager;
    [SerializeField] SceneEnum sceneEnum;
    [SerializeField] ShortcutState shortcutState;
    private void Start()
    {
        if (SaveSystem.tmpSave.shortcutDatas.Find(x=>x.Id==shortcutState.Id).IsUnlocked) UnlockShortcut?.Invoke();
            pickUpsManager.DestroyPickedPickUps(SaveSystem.tmpSave.sceneDatas[sceneEnum.sceneNum].wasPickUpPicked);
            puzzleManager.MarkPuzzlesAsSolved(SaveSystem.tmpSave.sceneDatas[sceneEnum.sceneNum].wasPuzzleSolved);
    }

    public void ChangePickUpState(int index,bool value)
    {
        SaveSystem.tmpSave.sceneDatas[sceneEnum.sceneNum].wasPickUpPicked[index] = value;
    }
    public void ChangePuzzleState(int index,bool value)
    {
        SaveSystem.tmpSave.sceneDatas[sceneEnum.sceneNum].wasPuzzleSolved[index] = value;
       
    }

    // used by unity event by destructable shortcuts in scene 2
    public void ChangeShortcutStateToUnlocked()
    {
        SaveSystem.tmpSave.shortcutDatas.Find(x => x.Id == shortcutState.Id).IsUnlocked = true;
    }
}
