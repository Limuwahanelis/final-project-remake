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
    [SerializeField] Collider2D[] _playerCols;
    private bool _canBePushed=true;
    private DamageType _invincibiltyType;
    private DamageType _pushInvincibiltyType;
    private IPlayerPusher _playerPusher;
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
    IEnumerator PushCor(IPlayerPusher pusher)
    {
        _canBePushed = false;
        yield return new WaitForSeconds(_invincibilityAfterHitDuration);
        _canBePushed = true;
        if (pusher != null)
        {
            pusher.ResumeCollisonsWithPlayer(_playerCols);
        }
    }
    IEnumerator InvincibilityCor()
    {
        _invincibiltyType=DamageType.ALL;
        _pushInvincibiltyType = DamageType.ALL;
        yield return new WaitForSeconds(_invincibilityAfterHitDuration);
        _invincibiltyType = DamageType.NONE;
        _pushInvincibiltyType = DamageType.NONE;
    }

    public void Push(PlayerHealthSystem.DamageType damageType, IPlayerPusher pusher = null)
    {
        if (player.isAlive)
        {
            if (_pushInvincibiltyType == damageType || _pushInvincibiltyType == DamageType.ALL) return;
            player.playerMovement.PushPlayer(pushHandle.GetPushVector() * pushForce,pusher);
            if (pusher != null) pusher.PreventCollisionWithPlayer(_playerCols);
            StartCoroutine(PushCor(pusher));
        }
    }
    public void Push(PlayerMovement.playerDirection direction, DamageType damageType)
    {
        if (player.isAlive)
        {
            if (_pushInvincibiltyType == damageType || _pushInvincibiltyType == DamageType.ALL) return;
            player.playerMovement.PushPlayer(direction, pushHandle.GetPushVector() * pushForce, null);
            StartCoroutine(PushCor(null));
        }
    }
    public void Push(PlayerMovement.playerDirection direction, DamageType damageType, IPlayerPusher pusher)
    {
        if (player.isAlive)
        {
            if (_pushInvincibiltyType == damageType || _pushInvincibiltyType == DamageType.ALL) return;
            player.playerMovement.PushPlayer(direction, pushHandle.GetPushVector() * pushForce, pusher);
            _playerPusher = pusher;
            StartCoroutine(PushCor(pusher));
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
