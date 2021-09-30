using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionInteractionComponent : MonoBehaviour
{
    [SerializeField]
    protected bool _pushCollidingObject;
    [SerializeField]
    protected bool _damageCollidingObject;
    [SerializeField]
    protected int damage;
    private void Start()
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(_pushCollidingObject)
        {
            IPushable toPush = collision.transform.GetComponentInParent<IPushable>();
            if (toPush != null) toPush.Push();
        }
        if(_damageCollidingObject)
        {
            IDamagable toDamage = collision.transform.GetComponentInParent<IDamagable>();
            if (toDamage != null) toDamage.TakeDamage(damage);
        }
    }
}
