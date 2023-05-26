using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    protected Boss _boss;

    public BossState (Boss boss)
    {
        _boss = boss;
    }
    public virtual void Attack() { }
    public virtual void AttackIsOver() { }

    public virtual void SetUpState() { }
    public abstract void Update();
}
