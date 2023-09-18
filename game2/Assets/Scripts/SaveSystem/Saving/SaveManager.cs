using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] PlayerHealthSystem _playerHealthSystem;
    [SerializeField] SceneEnum _sceneEnum;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {

        
    }
    public void Save(int saveIndex)
    {
        SaveSystem.SaveGame(_player, _playerHealthSystem,saveIndex, _sceneEnum.scene);
    }
}
