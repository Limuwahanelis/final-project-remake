using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickUpsManager : MonoBehaviour
{
    public List<PickUp> pickUpsInScene;
    [SerializeField]
    private SceneStateManager _sceneSaveManager;

    private void Awake()
    {
        for (int i = 0; i < pickUpsInScene.Count; i++)
        {
            pickUpsInScene[i].OnPickUp = SavePickUpState;
        }
    }

    private void SavePickUpState(PickUp pickUp)
    {
        _sceneSaveManager.ChangePickUpState(pickUpsInScene.IndexOf(pickUp), true);
    }
    public void DestroyPickedPickUps(List<bool> isPickUpPicked)
    {
        for(int i=0;i<pickUpsInScene.Count;i++)
        {
            if (isPickUpPicked[i]) Destroy(pickUpsInScene[i].gameObject);
        }
    }
}
