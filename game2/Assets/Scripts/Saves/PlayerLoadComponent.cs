using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoadComponent : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private PlayerHealthSystem _playerHealthSystem;
    public BoolReference loadSave;
    public IntReference saveToLoadIndex;
    // Start is called before the first frame update

    void Start()
    {
        if(loadSave.value)
        {
            PlayerData playerData = SaveSystem.LoadPlayerData(saveToLoadIndex.value);
            if (playerData == null) Debug.LogError("No save Data");
            else
            {
                _player.LoadData(playerData);
                _playerHealthSystem.LoadData(playerData);
            }
            loadSave.value = false;
        }
    }
}
