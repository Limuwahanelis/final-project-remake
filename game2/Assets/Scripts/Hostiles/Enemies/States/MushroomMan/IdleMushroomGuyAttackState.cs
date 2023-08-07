using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMushroomGuyAttackState : EnemyState
{
    private float _timer;
    private float _attackAnimDuration;
    private IdleMushroomGuyContext _context;
    public IdleMushroomGuyAttackState(IdleMushroomGuyContext context)
    {
        _context = context;
        _attackAnimDuration = context.anim.GetAnimationLength("Attack");
        _context.OnSetPlayerInRange += SetPlayerInRange;
        _context.OnSetPlayerBehind += SetPlayerBehind;
    }
    public override void Update()
    {
        if(_timer > _attackAnimDuration)
        {
            _context.ChangeState(new IdleMushroomGuyIdleState(_context));
        }
        _timer +=Time.deltaTime;
    }
    public override void SetUpState()
    {
        _context.anim.PlayAnimation("Attack");
    }
    
    private void SetPlayerInRange(bool value)
    {
        _context.isPlayerInRange = value;
    }
    public override void InterruptState()
    {
        _context.OnSetPlayerInRange -= SetPlayerInRange;
        _context.OnSetPlayerBehind -= SetPlayerBehind;
    }
    public override void Hit()
    {
        _context.ChangeState(new IdleMushroomGuyHitState(_context));
    }

    private void SetPlayerBehind()
    {
        _context.isPlayerBehind = true;
    }
}
