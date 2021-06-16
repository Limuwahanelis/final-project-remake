using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{

    public HealthBar hpBar;
    public int maxHP;
    public IntReference currentHP;
    // Start is called before the first frame update
    void Start()
    {
        hpBar.SetMaxHealth(maxHP);
        currentHP.value = maxHP;
        hpBar.SetHealth(currentHP.value);
    }
    public void TakeDamage(int dmg)
    {
        //StartCoroutine(InvincibilityCor());
        currentHP.value -= dmg;
        hpBar.SetHealth(currentHP.value);
    }

    public void Kill()
    {
        throw new NotImplementedException();
    }

    public void Knockback()
    {
        throw new NotImplementedException();
    }

    public void SlowDown(float slowDownFactorx, float slowDownFactory)
    {
        throw new NotImplementedException();
    }

    //public void IncreaseMaxHP(int amount)
    //{
    //    maxHP += amount;
    //    hpBar.SetMaxHealth(maxHP);
    //}
}
