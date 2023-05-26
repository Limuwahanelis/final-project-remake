using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaitingState : BossState
{
    BossState _nextState;
    float _timer = 0;
    float _timeToWait;
    public BossWaitingState(Boss boss, BossState nextState,float time) : base(boss)
    {
        _nextState = nextState;
        _timeToWait = time;
    }

    public override void Update()
    {
        _timer+=Time.deltaTime;
        if( _timer > _timeToWait ) _boss.ChangeState(new BossMoveToAttackPosState(_boss,_nextState));
    }
}
