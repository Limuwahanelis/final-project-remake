using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerState
{
    private Coroutine _jumpCor;
    public PlayerJumpingState(PlayerContext playerContext):base(playerContext)
    { }
    public override void Update()
    {
        
    }
    public override void Jump()
    {
    }
    public override void SetUpState()
    {
        _player.playerMovement.ChangeRb2DMat(_player.noFrictionMat);
        _player.anim.PlayAnimation("Jump");
        _player.isJumping = true;
        _jumpCor = _player.StartCoroutine(_player.WaitAndExecuteFunction(_player.anim.GetAnimationLength("Jump"), () =>
        {
            _player.playerMovement.Jump();
            _player.ChangeState(new PlayerInAirState(_player));
        }));
    }
    public override void InterruptState()
    {
        _player.StopCoroutine(_jumpCor);
    }


}
