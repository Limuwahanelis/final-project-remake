using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushedState : PlayerState
{
    private bool _isInAirAfterPush = false;
    public PlayerPushedState(PlayerContext playerContext):base(playerContext)
    {

    }
    public override void Update()
    {
        if(!_playerContext.playerChecks.IsOnGround && !_isInAirAfterPush)
        {
            _isInAirAfterPush =true;
            _playerContext.playerMovement.ChangeRb2DMat(_playerContext.noFrictionMat);
        }

        if (_playerContext.playerChecks.IsOnGround && _isInAirAfterPush)
        {
            _playerContext.playerMovement.ChangeRb2DMat(null);
            _playerContext.playerMovement.StopPlayer();
            _isInAirAfterPush = false;
            _playerContext.anim.SetAnimator(true);
            Debug.Log("retrun from push");
            _playerContext.ChangeState(new PlayerNormalState(_playerContext));
        }
    }

    public override void SetUpState()
    {
        
        _playerContext.anim.PlayAnimation("Idle");
        _playerContext.anim.SetAnimator(false);
        _playerContext.playerMovement.StopPlayer();
        _playerContext.playerCombat.ChangeSpriteToPushed();
    }
    public override void InterruptState()
    {
        base.InterruptState();
        _playerContext.anim.SetAnimator(true);
    }
}
