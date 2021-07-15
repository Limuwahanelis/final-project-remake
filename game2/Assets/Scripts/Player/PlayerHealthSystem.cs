using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : HealthSystem
{
    [SerializeField]
    private float _invincibilityAfterHitDuration;
    public override void TakeDamage(int dmg)
    {
        if (isInvincible) return;
        currentHP.value -= dmg;
        hpBar.SetHealth(currentHP.value);
        OnHitEvent?.Invoke();
        StartCoroutine(InvincibilityCor());
        if (currentHP.value < 0) Kill();

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
}
