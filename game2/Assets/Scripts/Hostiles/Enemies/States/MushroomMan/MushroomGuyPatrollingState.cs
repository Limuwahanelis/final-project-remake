using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGuyPatrollingState : PatrollingEnemyPatrolState
{
    //private MushroomGuyEnemy _mushroom;
    private bool _isHit=false;
    public MushroomGuyPatrollingState(MushroomGuyContext mushroomGuyContext):base(mushroomGuyContext)
    {
        //_mushroom = mushroomGuyContext;
        _context = mushroomGuyContext;
        mushroomGuyContext.OnSetPlayerInRange += SetPlayerInRange;
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
        _context.ChangeState(new MushroomGuyAttackState(_context as MushroomGuyContext));
    }
    public override void InterruptState()
    {
        base.InterruptState();
        (_context as MushroomGuyContext).OnSetPlayerInRange -= SetPlayerInRange;
    }
}
