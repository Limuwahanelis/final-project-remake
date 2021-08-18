using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem,IPushable
{
    [SerializeField]
    private float _invincibilityAfterHitDuration;
    public Player player;
    public PushHandle pushHandle;
    public float pushForce=2f;

    public override void TakeDamage(int dmg)
    {
        if (isInvincible) return;
        currentHP -= dmg;
        hpBar.SetHealth(currentHP);
        OnHitEvent?.Invoke();
        StartCoroutine(InvincibilityCor());
        if (currentHP < 0) Kill();

    }

    public override void Kill()
    {
        if (OnDeathEvent == null) Destroy(gameObject);
        else OnDeathEvent.Invoke();
    }

    IEnumerator InvincibilityCor()
    {
        isInvincible = true;
        yield return new WaitForSeconds(_invincibilityAfterHitDuration);
        isInvincible = false;
    }

    public void Push()
    {
        if (isInvincible) return;
        player.playerMovement.PushPlayer(pushHandle.GetPushVector() * pushForce);
    }
}
