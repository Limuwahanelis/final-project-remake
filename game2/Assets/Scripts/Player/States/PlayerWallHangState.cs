using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallHangState : PlayerState
{
    public PlayerWallHangState(PlayerContext playerContext):base(playerContext)
    {

    }
    public override void Update()
    {
    }

    public override void SetUpState()
    {
        _playerContext.anim.SetAnimator(false);
        _playerContext.playerMovement.StopPlayer();
        //_player.playerMovement.ChangeRb2DMat(null);
        //_player.playerMovement.SetGravityScale(0);
        _playerContext.playerMovement.SetRbYAxis(false);
        _playerContext.playerMovement.StopPlayer();
        _playerContext.playerMovement.ChangeSpriteToWallHang();
    }

    public override void Jump()
    {
        _playerContext.playerMovement.MovePlayer(0); // to don't stop _playerContext after jump
        _playerContext.playerMovement.ChangeSpriteToWallJump();//GetComponentInChildren<SpriteRenderer>().sprite = _playerContext.playerMovement.wallJumpSprite;
        //_player.playerMovement.SetGravityScale(2);
        _playerContext.playerMovement.SetRbYAxis(true);
        _playerContext.playerMovement.WallJump();//WallJump();
        _playerContext.playerMovement.RotatePlayerOppositeDirection();
        _playerContext.playerMovement.ChangeRb2DMat(_playerContext.noFrictionMat);
        _playerContext.anim.PlayAnimation("Jump");
        _playerContext.anim.SetAnimator(true);
        _playerContext.numberOfPerformedWallJumps++;
        _playerContext.ChangeState(new PlayerInAirState(_playerContext));
    }
    public override void InterruptState()
    {
        _playerContext.playerMovement.SetRbYAxis(true);
    }


}
