using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossState
{
    protected Boss _boss;
    protected BossContext _context;

    public BossState (Boss boss,BossContext bossContext)
    {
        _boss = boss;
        _context = bossContext;
    }
    public virtual void Attack() { }
    public virtual void AttackIsOver() { }

    public virtual void SetUpState() { }
    public abstract void Update();
}
