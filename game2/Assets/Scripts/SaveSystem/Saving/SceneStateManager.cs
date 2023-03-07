using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{

    [SerializeField] private PickUpsManager pickUpsManager;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private int sceneNum;

    private void Start()
    {
        if (SaveSystem.tmpSave.sceneDatas == null)
        {
            List<bool> PickUpvalues = new List<bool>();
            List<bool> puzzleValues = new List<bool>();
            for (int i = 0; i < pickUpsManager.pickUpsInScene.Count; i++)
            {
                PickUpvalues.Add(pickUpsManager.pickUpsInScene[i].pickedUp);
            }

            for (int i = 0; i < puzzleManager.Puzzels.Count; i++)
            {
                PickUpvalues.Add(false);
            }
            SceneData sceneData = new SceneData(PickUpvalues,puzzleValues);
            SaveSystem.tmpSave.CreateSceneDatas();
            SaveSystem.tmpSave.AddSceneData(sceneData);
        }
        else
        {
            pickUpsManager.DestroyPickedPickUps(SaveSystem.tmpSave.sceneDatas[sceneNum].wasPickUpPicked);
            puzzleManager.MarkPuzzlesAsSolved(SaveSystem.tmpSave.sceneDatas[sceneNum].wasPuzzleSolved);
        }
    }

    public void ChangePickUpState(int index,bool value)
    {
        SaveSystem.tmpSave.sceneDatas[sceneNum].wasPickUpPicked[index] = value;
    }
    public void ChangePuzzleState(int index,bool value)
    {
        SaveSystem.tmpSave.sceneDatas[sceneNum].wasPuzzleSolved[index] = value;
    }

}
