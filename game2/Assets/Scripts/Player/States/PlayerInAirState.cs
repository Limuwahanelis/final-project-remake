using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool _isMoving = false;
    private PlayerMovement.playerDirection _currentDirection;
    private PlayerMovement.playerDirection _previousDirection;

    public PlayerInAirState(PlayerContext playerContext) : base(playerContext)
    { 
    }
    public override void Update()
    {
            if (_playerContext.playerMovement.CheckIfPlayerIsFalling())
            {
                _playerContext.anim.PlayAnimation("Fall");
                _playerContext.playerMovement.ChangeRb2DMat(_playerContext.noFrictionMat);
            }
        if (_playerContext.playerChecks.IsOnGround && Mathf.Abs(_playerContext.playerMovement.GetPlayerVelocity().y)<0.0004 )//&& !_playerContext.isJumping && !_playerContext.isAirAttacking )
        {
            _playerContext.playerMovement.ChangeRb2DMat(null);
            _playerContext.playerMovement.StopPlayer();
            _playerContext.ChangeState(new PlayerNormalState(_playerContext));
            return;
        }
        if(_playerContext.playerChecks.IsNearWall && _playerContext.numberOfPerformedWallJumps<_playerContext.maximumNumberOfwallJumps && _isMoving && _currentDirection == _previousDirection)
        {
            if (_playerContext.abilityList.CheckIfAbilityIsUnlocked(AbilityList.Abilities.WALLHANG_ANDJUMP))
            {
                _playerContext.ChangeState(new PlayerWallHangState(_playerContext));
                return;
            }
        }
    }

    public override void Move(float direction)
    {
            if (direction == 0) _isMoving = false;
            else
            {
                _isMoving = true;
                _previousDirection = _currentDirection;
                _currentDirection = (PlayerMovement.playerDirection)direction;
            }
            _playerContext.playerMovement.MovePlayer(direction);
    }
    public override void Attack()
    {
        if (_playerContext.abilityList.CheckIfAbilityIsUnlocked(AbilityList.Abilities.AIR_ATTACK))
        {
            if (!_playerContext.canPerformAirAttack) return;
            _playerContext.ChangeState(new PlayerAirAttackState(_playerContext));

        }
    }
}
