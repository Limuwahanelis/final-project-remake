using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour,IDamagable
{
    public bool isInvincible;
    public HealthBar hpBar;
    public IntReference maxHP;
    public int currentHP;
    public Action OnHitEvent;
    public Action OnDeathEvent;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("G");
        hpBar.SetMaxHealth(maxHP.value);
        currentHP = maxHP.value;
        hpBar.SetHealth(currentHP);
    }
    public virtual void TakeDamage(int dmg)
    {
        Debug.Log("DMG");
        currentHP -= dmg;
        hpBar.SetHealth(currentHP);
        OnHitEvent?.Invoke();
        if (currentHP < 0) Kill();
    }

    public virtual void Kill()
    {
        if (OnDeathEvent == null) Destroy(gameObject);
        else OnDeathEvent.Invoke();
    }
}
