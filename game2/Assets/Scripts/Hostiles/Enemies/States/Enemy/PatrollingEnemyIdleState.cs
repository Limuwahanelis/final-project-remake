using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyIdleState : PatrollingEnemyState
{
    protected int _numberOfIdleCycles;
    protected EnemyState _previousState;
    protected float _timer = 0;

    public PatrollingEnemyIdleState(PatrollingEnemyContext patrollingEnemyContext, EnemyState previousState, int numberOfIdleCycles)
    {
        _context = patrollingEnemyContext;
        _previousState = previousState;
        _numberOfIdleCycles = numberOfIdleCycles;
    }

    public override void SetUpState()
    {
        if (_numberOfIdleCycles > 0 || _numberOfIdleCycles==-1) _context.anim.PlayAnimation("Idle");
        base.SetUpState();
    }
    public override void Update()
    {
        if (_numberOfIdleCycles == -1) return;
        if(_timer >= _numberOfIdleCycles * _context.anim.GetAnimationLength("Idle"))
        {
            _context.ChangeState(_previousState);
        }
        _timer +=Time.deltaTime;
    }

}
