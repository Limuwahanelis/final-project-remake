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
        if(!_playerContext.playerChecks.IsOnGround) _playerContext.ChangeState(new PlayerInAirState(_playerContext));
    }
    public override void Jump()
    {
    }
    public override void SetUpState()
    {
        _playerContext.playerMovement.ChangeRb2DMat(_playerContext.noFrictionMat);
        _playerContext.anim.PlayAnimation("Jump");
        //_playerContext.isJumping = true;
        _jumpCor = _playerContext.corutineHolder.StartCoroutine(_playerContext.WaitAndExecuteFunction(_playerContext.anim.GetAnimationLength("Jump"), () =>
        {
            _playerContext.playerMovement.Jump();
            //_playerContext.ChangeState(new PlayerInAirState(_playerContext));
        }));
    }
    public override void InterruptState()
    {
        _playerContext.corutineHolder.StopCoroutine(_jumpCor);
    }


}
