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

    public PlayerData(Player player,PlayerHealthSystem healthSystem,AbilityList list)
    {
        currentHP = healthSystem.currentHP.value;
        maxHP = healthSystem.maxHP.value;
        damage = player.playerCombat.attackDamage.value;
        position = player.transform.position;
        for(int i=0;i<abilities.Length;i++)
        {
            abilities[i] = list.GetAbility((AbilityList.Abilities)i);
        }
    }
}
