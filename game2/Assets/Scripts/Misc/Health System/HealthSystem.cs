using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour,IDamagable
{
    public bool isInvincible;
    public HealthBar hpBar;
    public int maxHP;
    public IntReference currentHP;
    public Action OnHitEvent;
    public Action OnDeathEvent;
    // Start is called before the first frame update
    void Start()
    {
        hpBar.SetMaxHealth(maxHP);
        currentHP.value = maxHP;
        hpBar.SetHealth(currentHP.value);
    }
    public virtual void TakeDamage(int dmg)
    {
        Debug.Log("Hot");
        currentHP.value -= dmg;
        hpBar.SetHealth(currentHP.value);
        OnHitEvent?.Invoke();
        if (currentHP.value < 0) Kill();
    }

    public virtual void Kill()
    {
        if (OnDeathEvent == null) Destroy(gameObject);
        else OnDeathEvent.Invoke();
    }
}
