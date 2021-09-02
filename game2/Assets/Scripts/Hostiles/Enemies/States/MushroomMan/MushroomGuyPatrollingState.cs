using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGuyPatrollingState : EnemyPatrollingState
{
    private MushroomGuyEnemy _mushroom;
    public MushroomGuyPatrollingState(MushroomGuyEnemy enemy):base(enemy)
    {
        _mushroom = enemy;
        
    }

    public override void Update()
    {
        base.Update();
        if(_mushroom.IsPlayerInRange)
        {
            _mushroom.ChangeState(new MushroomManAttackState(_mushroom));
        }
    }

    
}
