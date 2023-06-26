using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool _isMoving = false;
    private bool _hasAttacked = false;
    public PlayerInAirState(Player player) : base(player)
    { 
    }
    public override void Update()
    {
        if (!_player.isAirAttacking)
        {
            if (_player.playerMovement.CheckIfPlayerIsFalling())
            {
                _player.anim.PlayAnimation("Fall");
                _player.isJumping = false;
                _player.playerMovement.ChangeRb2DMat(_player.noFrictionMat);
            }
        }
        if (_player.isOnGround && Mathf.Abs(_player.playerMovement.GetPlayerVelocity().y)<0.0004 && !_player.isJumping )
        {
            _player.playerMovement.ChangeRb2DMat(null);
            _player.ChangeState(new PlayerNormalState(_player));
            return;
        }
        if(_player.isNearWall && !_player.hasWallJumped && _isMoving && _player.playerMovement.newPlayerDirection==_player.playerMovement.oldPlayerDirection)
        {
            if (_player.abilities.CheckIfAbilityIsUnlocked(AbilityList.Abilities.WALLHANG_ANDJUMP))
            {
                if (_player.isAirAttacking) _player.playerCombat.StopAirAttack();
                _player.ChangeState(new PlayerWallHangState(_player));
                return;
            }
        }
    }

    public override void Move(float direction)
    {

        if (!_player.isAirAttacking)
        {
            if (direction == 0) _isMoving = false;
            else
            {
                _isMoving = true;
            }
            _player.playerMovement.MovePlayer(direction);
        }
    }
    public override void Attack()
    {
        if (_player.abilities.CheckIfAbilityIsUnlocked(AbilityList.Abilities.AIR_ATTACK))
        {
            if (_hasAttacked) return;
            _hasAttacked = true;
            _player.audioManager.PlayAirAttackSound();
            _player.playerCombat.AirAttack();
        }
    }
}
