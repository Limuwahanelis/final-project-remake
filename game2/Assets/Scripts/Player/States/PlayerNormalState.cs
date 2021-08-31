using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalState : PlayerState
{

    private bool _isMoving = false;
    public PlayerNormalState(Player player) : base(player)
    {
        _player.hasWallJumped = false;
    }
    public override void Jump()
    {
        _player.isAttacking = false;
        _isMoving = false;
        _player.ChangeState(new PlayerJumpingState(_player));
        _player.currentState.Jump();
    }

    public override void Move(float direction)
    {
        if (!_player.isAttacking)
        {
            if (direction == 0) _isMoving = false;
            else
            {
                _isMoving = true;
                _player.anim.PlayAnimation("Walk");
                _player.audioManager.PlayWalkSound();
            }
            _player.playerMovement.MovePlayer(direction);
        }
    }

    public override void Attack()
    {
        if (_player.isAttacking) return;
        _player.audioManager.PlayNormalAttackSound();
        _player.isAttacking = true;
        _player.playerCombat.Attack(this);
    }

    public override void AttackIsOver()
    {
        _player.isAttacking = false;
    }
    public override void Update()
    {
        if(!_isMoving && !_player.isAttacking) _player.anim.PlayAnimation("Idle");
        if (!_player.isOnGround) _player.ChangeState(new PlayerInAirState(_player));
    }
    public override void Slide()
    {
        _player.ChangeState(new PlayerSlideState(_player));
    }
}
