using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyIdleState : PatrollingEnemyState
{
    protected int _numberOfIdleCycles;
    protected EnemyState _previousState;
    protected EnemyState _nextState;
    protected float _timer = 0;

    public PatrollingEnemyIdleState(PatrollingEnemyContext patrollingEnemyContext, EnemyState previousState, int numberOfIdleCycles, EnemyState nextState=null)
    {
        _context = patrollingEnemyContext;
        _previousState = previousState;
        _numberOfIdleCycles = numberOfIdleCycles;
        _nextState = nextState;
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
            if(_nextState == null) _context.ChangeState(_previousState);
            else _context.ChangeState(_nextState);

        }
        _timer +=Time.deltaTime;
    }

}
