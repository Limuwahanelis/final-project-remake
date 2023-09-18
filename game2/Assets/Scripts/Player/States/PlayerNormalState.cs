using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalState : PlayerState
{

    private bool _isMoving = false;
    float lastDirection = -2;
    public PlayerNormalState(PlayerContext playerContext) : base(playerContext)
    {
    }
    public override void Jump()
    {
        _isMoving = false;
        _playerContext.playerMovement.StopPlayerOnXAxis();
        _playerContext.ChangeState(new PlayerJumpingState(_playerContext));
    }

    public override void Move(float direction)
    {
        //Debug.Log(direction);
            if (direction ==0) _isMoving = false;
            else _isMoving = true;
            _playerContext.playerMovement.MovePlayer(direction);  
    }

    public override void Attack()
    {
        _playerContext.ChangeState(new PlayerAttackState(_playerContext));
    }

    public override void Update()
    {
        if(!_isMoving) _playerContext.anim.PlayAnimation("Idle");
        else
        {
            _playerContext.anim.PlayAnimation("Walk");
            _playerContext.audioManager.PlayWalkSound();
        }
        if (!_playerContext.playerChecks.IsOnGround) _playerContext.ChangeState(new PlayerInAirState(_playerContext));
    }
    public override void Slide()
    {
        _playerContext.ChangeState(new PlayerSlideState(_playerContext));
    }
    public override void DropBomb()
    {
        if(_playerContext.abilityList.CheckIfAbilityIsUnlocked(AbilityList.Abilities.BOMB_DROP)) _playerContext.ChangeState(new PlayerDropBombState(_playerContext));

    }
    public override void SetUpState()
    {
        _playerContext.canPerformAirAttack = true;
        _playerContext.numberOfPerformedWallJumps = 0;
    }
}
