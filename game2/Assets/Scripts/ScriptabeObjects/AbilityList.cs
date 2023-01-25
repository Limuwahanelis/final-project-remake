using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ability list")]
public class AbilityList : ScriptableObject
{
    public enum Abilities
    {
        WALLHANG_ANDJUMP,
        AIR_ATTACK,
        BOMB_DROP
    }
    public List<Ability> abilities = new List<Ability>();
    //public int findAbilityIndex(Abilities ability)
    //{
    //    int toReturn = -1;
    //    toReturn = abilities.FindIndex((x) => x.name == ability);
    //    if (toReturn == -1) Debug.Log("No Such Ability");
    //    return toReturn;
    //}

    public bool CheckIfAbilityIsUnlocked(Abilities ability)
    {
        if(abilities.Find(x=>x.name==ability).isUnlocked) return true;
        return false;
    }

    public void UnlockAbility(Abilities ability)
    {
        abilities.Find(x => x.name == ability).UnlockAbility();
    }
    public void LockAbility(Abilities ability)
    {
        abilities.Find(x => x.name == ability).LockAbility();
    }
}
