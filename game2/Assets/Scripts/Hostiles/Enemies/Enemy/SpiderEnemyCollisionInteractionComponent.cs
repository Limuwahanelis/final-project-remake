using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemyCollisionInteractionComponent : CollisionInteractionComponent,IPlayerPusher
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collision(collision);
        //if(otherCol.GetComponent<IPushable>()!=null) otherCol = collision.collider;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {

        Collision(collision);
       // Physics2D.IgnoreCollision(otherCol, this.GetComponent<Collider2D>(), true);
    }
    private void Collision(Collision2D collision)
    {
        float dir = collision.transform.position.x - transform.position.x;
        PlayerMovement.playerDirection pushDir;
        if (dir > 0) pushDir = PlayerMovement.playerDirection.RIGHT;
        else pushDir = PlayerMovement.playerDirection.LEFT;

        if (_pushCollidingObject)
        {
            //
            IPushable toPush = collision.transform.GetComponentInParent<IPushable>();
            if (toPush != null) toPush.Push(PlayerHealthSystem.DamageType.ENEMY, this);
        }
        if (_damageCollidingObject)
        {
            IDamagable toDamage = collision.transform.GetComponentInParent<IDamagable>();
            if (toDamage != null) toDamage.TakeDamage(damage, PlayerHealthSystem.DamageType.ENEMY);

        }
    }
    public void SetCollisionDamage(int dmg)
    {
        damage = dmg;
    }

    public void ResumeCollisonsWithPlayer(Collider2D[] playerCols)
    {
        foreach(Collider2D col in playerCols)
        {
            Physics2D.IgnoreCollision(col, GetComponent<Collider2D>(), false);
        }
       
    }

    public void PreventCollisionWithPlayer(Collider2D[] playerCols)
    {
        foreach (Collider2D col in playerCols)
        {
            Physics2D.IgnoreCollision(col, GetComponent<Collider2D>(), true);
        }
    }
}
