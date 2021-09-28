using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ability
{
    public AbilityList.Abilities name;
    [SerializeField]
    private bool unlocked;
    public bool isUnlocked
    {
        get { return unlocked; }
    }
    
    public void UnlockAbility()
    {
        unlocked = true;
    }

    public void LockAbility()
    {
        unlocked = false;
    }
}
