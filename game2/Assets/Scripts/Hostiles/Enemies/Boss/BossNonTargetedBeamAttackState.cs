using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNonTargetedBeamAttackState : BossState
{
    public BossNonTargetedBeamAttackState(Boss boss,BossContext context) : base(boss,context)
    {
    }

    public override void Update()
    {
    }

    public override void SetUpState()
    {
        _context.crystals.OnAttackEnded += ChangeState;
        _context.crystals.StartCrystalAttacks();
    }

    private void ChangeState()
    {
        _context.crystals.OnAttackEnded -= ChangeState;
        switch (_context.attackPatten)
        {
            case 1: _boss.ChangeState(new PlayerPosBeamAttackBosState(_boss, _context)); break;
            case 2: _boss.ChangeState(new BossCircleMissileAttackState(_boss,_context)); break;
            case 3: _boss.ChangeState(new BossPlayerFollowMissileAttackState(_boss, _context)); break;
            default: break;
        }
    }
}
