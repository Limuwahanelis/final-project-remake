using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEnemy : PatrollingEnemy
{
    private EnemyPatrollingState _patrolState;

    // Start is called before the first frame update
    private void Awake()
    {
        SetUpComponents();
    }
    void Start()
    {
        if (patrolPoints.Count < 2)
        {
            Debug.LogError("fsaf");
            return;
        }
        _patrolState = new EnemyPatrollingState(this);
        state = _patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        _patrolState.Update();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
            IDamagable player = collision.transform.GetComponent<IDamagable>();
            //player.Knockback();
            player.TakeDamage(dmg);
    }

}
