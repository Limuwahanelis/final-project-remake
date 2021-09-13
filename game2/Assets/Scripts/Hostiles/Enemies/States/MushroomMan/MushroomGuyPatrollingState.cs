using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGuyPatrollingState : EnemyPatrollingState
{
    private MushroomGuyEnemy _mushroom;
    private bool _isHit=false;
    public MushroomGuyPatrollingState(MushroomGuyEnemy enemy):base(enemy)
    {
        _mushroom = enemy;
        
    }

    public override void Update()
    {
        base.Update();
        if(_mushroom.IsPlayerInRange && !_isHit)
        {
            _mushroom.ChangeState(new MushroomManAttackState(_mushroom));
        }
    }

    public override void Hit()
    {
        _isHit = true;
        _anim.PlayAnimation("Hit");
        _enemy.StartCoroutine(_enemy.WaitAndExecuteFunction(_anim.GetAnimationLength("Hit"), () =>
        {
            if (!_mushroom.IsPlayerInRange)
            {
                _mushroom.Rotate();
            }
            _isHit = false;

        }));

        


    }
}
