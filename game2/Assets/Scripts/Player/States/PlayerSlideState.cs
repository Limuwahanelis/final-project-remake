using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerState
{
    float _time = 0;
    public PlayerSlideState(PlayerContext playerContext) : base(playerContext)
    {
    }
    public override void Update()
    {
        _playerContext.playerMovement.MovePlayerForward();
        if(_time>_playerContext.playerMovement.slideTime)
        {
            if (!_playerContext.playerChecks.IsNearCeiling)
            {
                _playerContext.ChangeState(new PlayerNormalState(_playerContext));
                _playerContext.playerMovement.StopPlayer();
                _playerContext.SetSlideMode(false);
            }
        }
        if(_playerContext.playerChecks.CheckForSlideWall())
        {
            _playerContext.SetSlideMode(false);
            _playerContext.playerMovement.StopPlayer();
            _playerContext.ChangeState(new PlayerNormalState(_playerContext));
        }
        _time += Time.deltaTime;
    }
    public override void SetUpState()
    {
        _playerContext.SetSlideMode(true);
        _playerContext.anim.PlayAnimation("Slide");

    }

}
