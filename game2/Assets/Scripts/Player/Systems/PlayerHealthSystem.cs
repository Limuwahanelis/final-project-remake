using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem,IPushable
{
    [Flags]
    public enum DamageType
    {
        NONE = 0,
        ENEMY = 2,
        MISSILE = 4,
        TRAPS = 8,
        ALL = 16,
    }

    [SerializeField] float _invincibilityAfterHitDuration;
    private bool _canBePushed=true;
    private DamageType _invincibiltyType;
    private DamageType _pushInvincibiltyType;
    public Player player;
    public Ringhandle pushHandle;
    public float pushForce=2f;
    private new void Start()
    {
        hpBar.SetMaxHealth(maxHP.value);
        hpBar.SetHealth(currentHP.value);
    }
    public void SetInvincibility(DamageType invincibiltyType)
    {
        _invincibiltyType = invincibiltyType;
    }
    public void SetPushInvincibility(DamageType invincibiltyType)
    {
        _pushInvincibiltyType = invincibiltyType;
    }
    public override void TakeDamage(int dmg, DamageType damageType)
    {
        if (player.isAlive)
        {
            if (_invincibiltyType==damageType || _invincibiltyType==DamageType.ALL) return;
            currentHP.value -= dmg;
            hpBar.SetHealth(currentHP.value);
            if (currentHP.value < 0) Kill();
            else OnHitEvent?.Invoke();
            player.currentState.OnHit();
            StartCoroutine(InvincibilityCor());
        }

    }

    public override void Kill()
    {
        if (OnDeathEvent == null) Destroy(gameObject);
        else OnDeathEvent.Invoke();
    }
    IEnumerator PushCor()
    {
        _canBePushed = false;
        yield return new WaitForSeconds(_invincibilityAfterHitDuration);
        _canBePushed = true;
    }
    IEnumerator InvincibilityCor()
    {
        _invincibiltyType=DamageType.ALL;
        _pushInvincibiltyType = DamageType.ALL;
        yield return new WaitForSeconds(_invincibilityAfterHitDuration);
        _invincibiltyType = DamageType.NONE;
        _pushInvincibiltyType = DamageType.NONE;
    }

    public void Push(PlayerHealthSystem.DamageType damageType)
    {
        if (player.isAlive)
        {
            if (_pushInvincibiltyType == damageType || _pushInvincibiltyType == DamageType.ALL) return;
            player.playerMovement.PushPlayer(pushHandle.GetPushVector() * pushForce);
            StartCoroutine(PushCor());
        }
    }
    public void Push(PlayerMovement.playerDirection direction, DamageType damageType)
    {
        if (player.isAlive)
        {
            if (_pushInvincibiltyType == damageType || _pushInvincibiltyType == DamageType.ALL) return;
            player.playerMovement.PushPlayer(direction, pushHandle.GetPushVector() * pushForce);
            StartCoroutine(PushCor());
        }
    }
    public void IncreaseHealthBarMaxValue()
    {
        hpBar.SetMaxHealth(maxHP.value);
        hpBar.SetHealth(currentHP.value);
    }

    public void LoadData(PlayerData playerData)
    {
        maxHP.value = playerData.maxHP;
        currentHP.value = playerData.currentHP;
    }
}
