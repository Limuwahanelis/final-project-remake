using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemyCollisionInteractionComponent : CollisionInteractionComponent
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        float dir = collision.transform.position.x - transform.position.x;
        PlayerMovement.playerDirection pushDir;
        if (dir > 0) pushDir = PlayerMovement.playerDirection.RIGHT;
        else pushDir = PlayerMovement.playerDirection.LEFT;

        if (_pushCollidingObject)
        {
            IPushable toPush = collision.transform.GetComponentInParent<IPushable>();
            if (toPush != null) toPush.Push(pushDir,PlayerHealthSystem.DamageType.ENEMY);
        }
        if (_damageCollidingObject)
        {
            IDamagable toDamage = collision.transform.GetComponentInParent<IDamagable>();
            if (toDamage != null) toDamage.TakeDamage(damage,PlayerHealthSystem.DamageType.ENEMY);
        }
    }

    public void SetCollisionDamage(int dmg)
    {
        damage = dmg;
    }
}
