using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AbilityManager : MonoBehaviour
{
    public enum Abilities
    {
        WALLHANG_ANDJUMP,
        AIR_ATTACK
    }
    public bool[] unlockedAbilities=new bool[2];

    public void UnlockAbility(Abilities abilityToUnlock)
    {
        unlockedAbilities[(int)abilityToUnlock] = true;
    }
}
