using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManIdleState : EnemyState
{
    public MushroomManIdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public override void Update()
    {
        enemy.GetAnimationManager().PlayAnimation("Idle");
    }
}
