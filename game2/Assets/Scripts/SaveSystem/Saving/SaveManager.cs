using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private PlayerHealthSystem _playerHealthSystem;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {

        
    }
    public void Save(int saveIndex)
    {
        SaveSystem.SaveGame(_player, _playerHealthSystem,saveIndex);
    }
}
