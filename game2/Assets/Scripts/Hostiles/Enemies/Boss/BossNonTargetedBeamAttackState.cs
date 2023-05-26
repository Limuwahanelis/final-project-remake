using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNonTargetedBeamAttackState : BossState
{
    public BossNonTargetedBeamAttackState(Boss boss) : base(boss)
    {
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    public override void SetUpState()
    {
        _boss.crystals.OnAttackEnded += ChangeState;
        _boss.crystals.StartCrystalAttacks();
    }

    private void ChangeState()
    {
        _boss.crystals.OnAttackEnded -= ChangeState;
        switch (_boss.attackPatten)
        {
            case 1: _boss.ChangeState(new PlayerPosBeamAttackBosState(_boss)); break;
            case 2: _boss.ChangeState(new BossCircleMissileAttackState(_boss)); break;
            case 3: _boss.ChangeState(new BossPlayerFollowMissileAttackState(_boss)); break;
            default: break;
        }
    }
}
