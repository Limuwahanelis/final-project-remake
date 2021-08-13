using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlideState : PlayerState
{
    private bool _isSliding = false;
    public PlayerSlideState(Player player) : base(player)
    {
    }
    public override void Update()
    {
        _player.playerMovement.MovePlayer(_player.mainBody.transform.localScale.x);
    }
    public override void SetUpState()
    {
        _player.slideColliders.SetActive(true);
        _player.normalColliders.SetActive(false);
        _player.anim.PlayAnimation("Slide");

        _player.StartCoroutine(_player.WaitAndExecuteFunction(_player.playerMovement.slideTime, () =>
        {
            _player.ChangeState(new PlayerNormalState(_player));
            _player.playerMovement.StopPlayer();
            _player.StopAllCoroutines();
        }));
        _player.StartCoroutine(_player.playerChecks.CheckForWallDuringSlideCor());
    }

}
