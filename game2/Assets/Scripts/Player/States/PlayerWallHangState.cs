using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallHangState : PlayerState
{
    public PlayerWallHangState(Player player):base(player)
    {

    }
    public override void Update()
    {
        //throw new System.NotImplementedException();
    }

    public override void SetUpState()
    {
        _player.anim.SetAnimator(false);
        _player.isJumping = false;
        _player.playerMovement.SetGravityScale(0);
        _player.playerMovement.StopPlayer();
        _player.GetComponentInChildren<SpriteRenderer>().sprite = _player.playerMovement.wallHangSprite;
    }

    public override void Jump()
    {
        _player.playerMovement.MovePlayer(0); // to don't stop _player after jump
        _player.GetComponentInChildren<SpriteRenderer>().sprite = _player.playerMovement.wallJumpSprite;
        _player.playerMovement.SetGravityScale(2);
        _player.playerMovement.Jump();//WallJump();
        _player.playerMovement.RotatePlayer((int)-_player.mainBody.transform.localScale.x);
        _player.playerMovement.ChangeRb2DMat(_player.noFrictionMat);
        _player.hasWallJumped = true;
        _player.anim.PlayAnimation("Jump");
        _player.anim.SetAnimator(true);

        _player.ChangeState(new PlayerInAirState(_player));
    }


}
