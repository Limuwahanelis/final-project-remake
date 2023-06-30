using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUnlockLoadComponent : MonoBehaviour
{
    //public BoolReference isGameLoaded;

    [SerializeField] AbilityUnlock _abilityUnlock;
    // Start is called before the first frame update
    void Start()
    {
            if (_abilityUnlock.abilityList.CheckIfAbilityIsUnlocked(_abilityUnlock.ability)) Destroy(gameObject);
    }
}
