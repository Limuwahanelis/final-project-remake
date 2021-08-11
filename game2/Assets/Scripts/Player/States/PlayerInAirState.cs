using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool _isMoving = false;
    private bool _isFalling = false;
    private bool _hasAttacked = false;
    public PlayerInAirState(Player player) : base(player)
    { }
    public override void Update()
    {
        if (!_player.isAirAttacking)
        {
            if (_player.playerMovement.CheckIfPlayerIsFalling())
            {
                _isFalling = true;
                _player.anim.PlayAnimation("Fall");
            }
            else _isFalling = false;
        }
        if (_player.isOnGround) _player.ChangeState(new PlayerNormalState(_player));
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
        if (_hasAttacked) return;
        _hasAttacked = true;
        _player.playerCombat.AirAttack();
    }
}
