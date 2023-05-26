using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeadState : BossState
{
    public BossDeadState(Boss boss) : base(boss)
    {
    }

    public override void Update()
    {
        
    }

    public override void SetUpState()
    {
       _boss.crystals.DestroyCrystals();
        _boss.PlayAnimation("Dead");
        _boss.StopAllCoroutines();
        _boss.WaitAndExecuteFunction(_boss.GetAnimationManager().GetAnimationLength("Dead"), () => { _boss.ShowCredits(); });
    }

}
