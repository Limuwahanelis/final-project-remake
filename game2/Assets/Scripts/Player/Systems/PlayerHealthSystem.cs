using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem,IPushable
{
    [SerializeField] float _invincibilityAfterHitDuration;
    private bool _canBePushed=true;
    public Player player;
    public Ringhandle pushHandle;
    public float pushForce=2f;
    private new void Start()
    {
        hpBar.SetMaxHealth(maxHP.value);
        hpBar.SetHealth(currentHP.value);
    }

    public override void TakeDamage(int dmg)
    {
        if (player.isAlive)
        {
            if (isInvincible) return;
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
        isInvincible = true;
        yield return new WaitForSeconds(_invincibilityAfterHitDuration);
        isInvincible = false;
    }

    public void Push()
    {
        if (player.isAlive)
        {
            if (!_canBePushed) return;
            player.playerMovement.PushPlayer(pushHandle.GetPushVector() * pushForce);
            StartCoroutine(PushCor());
        }
    }
    public void Push(PlayerMovement.playerDirection direction)
    {
        if (player.isAlive)
        {
            if (!_canBePushed) return;
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
