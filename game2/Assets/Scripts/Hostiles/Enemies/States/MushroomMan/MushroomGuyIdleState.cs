using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGuyIdleState : PatrollingEnemyIdleState
{
    public MushroomGuyIdleState(MushroomGuyContext context,EnemyState previousState,int numberOfIdleCycles,EnemyState nextState=null):base(context,previousState,numberOfIdleCycles,nextState)
    {
        context.OnSetPlayerInRange += SetPlayerInRange;
    }

    public override void Update()
    {
        if (_numberOfIdleCycles == -1) return;
        if (_timer >= _numberOfIdleCycles * _context.anim.GetAnimationLength("Idle"))
        {
            if ((_context as MushroomGuyContext).isPlayerInRange) _context.ChangeState(new MushroomGuyAttackState(_context as MushroomGuyContext));
            else
            {
                if (_nextState == null) _context.ChangeState(new MushroomGuyPatrollingState((_context as MushroomGuyContext)));
                else _context.ChangeState(_nextState);

            }
            
        }
        _timer += Time.deltaTime;
    }
    public override void SetUpState()
    {
        base.SetUpState();
    }
    public override void Hit()
    {
        base.Hit();
        _context.ChangeState(new MushroomGuyHitState(_context as MushroomGuyContext));
    }
    private void SetPlayerInRange(bool value)
    {
        (_context as MushroomGuyContext).isPlayerInRange = value;
    }
}
