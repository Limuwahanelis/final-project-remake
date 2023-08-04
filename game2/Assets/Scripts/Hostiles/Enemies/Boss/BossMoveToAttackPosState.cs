using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveToAttackPosState : BossState
{
    BossState _nextState;
    public BossMoveToAttackPosState(Boss boss,BossContext context ,BossState nextState) : base(boss,context)
    {
        _nextState = nextState;
        _context = context;
    }

    public override void Update()
    {
        MoveToPosition(_context.attackPos);
    }
    public override void SetUpState()
    {
        
    }
    void MoveToPosition(Vector3 pos)
    {
        float step = _context.speed * Time.deltaTime;
        _boss.transform.position = Vector3.MoveTowards(_boss.transform.position, pos, step);
        if (Vector3.Distance(_boss.transform.position, pos) > 0.001f) return;
        _boss.ChangeState(new BossNonTargetedBeamAttackState(_boss,_context));

    }

}
