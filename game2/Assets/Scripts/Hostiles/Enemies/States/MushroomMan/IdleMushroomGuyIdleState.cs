using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMushroomGuyIdleState : EnemyState
{
    private IdleMushroomGuyContext _context;
    private float _timer;
    private float _idleAnimLength;
    public IdleMushroomGuyIdleState(IdleMushroomGuyContext context)
    {
        _context = context;
        _idleAnimLength = context.anim.GetAnimationLength("Idle");
        _context.OnSetPlayerBehind += SetPlayerBehind;
        _context.OnSetPlayerInRange += SetPlayerInRange;
    }
    public override void SetUpState()
    {
        _context.anim.PlayAnimation("Idle");
    }
    public override void Update()
    {
        if(_timer > _idleAnimLength)
        {
            if (_context.isPlayerInRange) _context.ChangeState(new IdleMushroomGuyAttackState(_context));
            else if (_context.isPlayerBehind) SetPlayerBehind();
        }
        _timer += Time.deltaTime;
    }

    private void SetPlayerBehind()
    {
        _context.isPlayerBehind = false;
        _context.Rotate();
    }
    private void SetPlayerInRange(bool value)
    {
        _context.isPlayerInRange = value;
    }

    public override void InterruptState()
    {
        _context.OnSetPlayerBehind -= SetPlayerBehind;
    }
    public override void Hit()
    {
        _context.ChangeState(new IdleMushroomGuyHitState(_context));
    }
}
