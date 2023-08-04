using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGuyHitState : PatrollingEnemyState
{
    private float _timer=0f;
    private float _hitAnimLength;
    public MushroomGuyHitState(MushroomGuyContext context)
    {
        _context = context;
    }
    public override void SetUpState()
    {
        _context.anim.PlayAnimation("Hit");
        _hitAnimLength = _context.anim.GetAnimationLength("Hit");
    }
    public override void Update()
    {
        if(_timer>=_hitAnimLength)
        {
            if ((_context as MushroomGuyContext).isPlayerInRange) _context.ChangeState(new MushroomGuyIdleState(_context as MushroomGuyContext, new MushroomGuyAttackState(_context as MushroomGuyContext),1));
            else
            {
                (_context as MushroomGuyContext).Rotate();
                _context.ChangeState(new MushroomGuyAttackState(_context as MushroomGuyContext));
            }

        }
        _timer += Time.deltaTime;
    }
}
