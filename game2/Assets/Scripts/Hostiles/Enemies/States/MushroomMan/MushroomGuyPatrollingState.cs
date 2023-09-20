using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGuyPatrollingState : PatrollingEnemyPatrolState
{
    private bool _isHit=false;
    public MushroomGuyPatrollingState(MushroomGuyContext mushroomGuyContext):base(mushroomGuyContext)
    {
        _context = mushroomGuyContext;
    }
    public override void SetUpState()
    {
        base.SetUpState();
        (_context as MushroomGuyContext).OnSetPlayerInRange += SetPlayerInRange;
    }
    public override void Update()
    {
        base.Update();
        if((_context as MushroomGuyContext).isPlayerInRange && !_isHit)
        {
            _context.ChangeState(new MushroomGuyAttackState(_context as MushroomGuyContext));
        }
    }

    public override void Hit()
    {
        _context.ChangeState(new MushroomGuyHitState(_context as MushroomGuyContext));

    }
    private void SetPlayerInRange(bool isPlayerInRange)
    {
        (_context as MushroomGuyContext).isPlayerInRange = isPlayerInRange;
    }
    protected override void StayIdleAtPatrolPoint()
    {
        _context.ChangeState(new MushroomGuyIdleState((_context as MushroomGuyContext), this, _context.NumberOfIdleCycles));
    }
    public override void InterruptState()
    {
        base.InterruptState();
        (_context as MushroomGuyContext).OnSetPlayerInRange -= SetPlayerInRange;
    }
}
