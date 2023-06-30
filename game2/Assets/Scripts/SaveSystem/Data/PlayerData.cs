using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class PlayerData
{
    public int currentHP;
    public int maxHP;
    public int damage;
    public Vector3 position;
    public bool[] abilities =new bool[Enum.GetNames(typeof(AbilityList.Abilities)).Length];

    public PlayerData()
    {

    }
    public PlayerData(Player player,PlayerHealthSystem healthSystem,AbilityList list)
    {
        currentHP = healthSystem.currentHP.value;
        maxHP = healthSystem.maxHP.value;
        damage = player.playerCombat.attackDamage.value;
        position = player.transform.position;
        for(int i=0;i<abilities.Length;i++)
        {
            abilities[i] = list.CheckIfAbilityIsUnlocked((AbilityList.Abilities)i);
        }
    }
    //public PlayerData(int currentHP, int maxHP,int dmg,Vector3 position, AbilityList list)
    //{
    //    currentHP = healthSystem.currentHP.value;
    //    maxHP = healthSystem.maxHP.value;
    //    damage = player.playerCombat.attackDamage.value;
    //    position = player.transform.position;
    //    for (int i = 0; i < abilities.Length; i++)
    //    {
    //        abilities[i] = list.CheckIfAbilityIsUnlocked((AbilityList.Abilities)i);
    //    }
    //}


    public PlayerData(PlayerData playerData)
    {
        currentHP = playerData.currentHP;
        maxHP = playerData.maxHP;
        damage = playerData.damage;
        position = playerData.position;
        for (int i = 0; i < playerData.abilities.Length; i++)
        {
            abilities[i] = playerData.abilities[i];
        }
    }
    public void UnlockAbility(AbilityList.Abilities ability)
    {
        abilities[((int)ability)] = true;
    }
}
