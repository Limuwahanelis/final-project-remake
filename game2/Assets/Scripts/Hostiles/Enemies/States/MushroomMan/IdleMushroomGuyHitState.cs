using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMushroomGuyHitState : EnemyState
{
    private float _timer = 0f;
    private float _hitAnimLength;
    private IdleMushroomGuyContext _context;
    public IdleMushroomGuyHitState(IdleMushroomGuyContext context)
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
        if (_timer >= _hitAnimLength)
        {
            if (_context.isPlayerInRange) _context.ChangeState(new IdleMushroomGuyIdleState(_context));
            else
            {
                _context.Rotate();
                _context.ChangeState(new IdleMushroomGuyAttackState(_context));
            }

        }
        _timer += Time.deltaTime;
    }
}
