using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveToVulnerablePosState : BossState
{
    BossState _nextState;
    bool _goToAttackPosNext = true;
    /// <summary
    /// fasf
    /// </summary>
    /// <param name="boss"></param>
    /// <param name="nextState">state which should be entered after "vulnerable pos -> wait -> attack pos" cycle if goToAttackPosNext param is se to true.
    /// Otherwise goes to this state</param>
    /// <param name="goToAttackPosNext"> </param>
    public BossMoveToVulnerablePosState(Boss boss,BossState nextState, bool goToAttackPosNext=true) : base(boss)
    {
        _nextState = nextState;
        _goToAttackPosNext = goToAttackPosNext;
    }

    public override void Update()
    {
        MoveToPosition(_boss.vulnerablePos);
    }
    public override void SetUpState()
    {
        
    }
    void MoveToPosition(Vector3 pos)
    {
        float step = _boss.speed * Time.deltaTime;
        _boss.transform.position = Vector3.MoveTowards(_boss.transform.position, pos, step);
        if (Vector3.Distance(_boss.transform.position, pos) > 0.001f) return;
        if(_goToAttackPosNext) _boss.ChangeState(new BossWaitingState(_boss,_nextState,_boss.vulnerableTime));
        else _boss.ChangeState(_nextState);

    }
}
