using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private Enemy _enemy;
    public EnemyIdleState(Enemy enemy)
    {
        this._enemy = enemy;
    }
    public override void Update()
    {
        _enemy.GetAnimationManager().PlayAnimation("Idle");
    }
}
