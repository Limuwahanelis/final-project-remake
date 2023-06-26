using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushedState : PlayerState
{
    public PlayerPushedState(Player player):base(player)
    {
        _player = player;

        
        //_player.anim.PlayAnimation("Empty");

        
    }
    public override void Update()
    {

        if (_player.isOnGround && _player.isInAirAfterPush)
        {
            _player.playerMovement.ChangeRb2DMat(null);
            _player.playerMovement.StopPlayer();
            _player.isInAirAfterPush = false;
            _player.anim.SetAnimator(true);
            Debug.Log("retrun from push");
            _player.ChangeState(new PlayerNormalState(_player));
        }
    }

    public override void SetUpState()
    {
        
        _player.anim.PlayAnimation("Idle");
        _player.anim.SetAnimator(false);
        _player.playerMovement.StopPlayer();
        _player.GetComponentInChildren<SpriteRenderer>().sprite = _player.playerCombat.playerHitSprite;
        _player.playerMovement.StartCoroutine(PushCor());
    }
    public IEnumerator PushCor()
    {
        while (_player.isOnGround) yield return null;
        _player.isInAirAfterPush = true;
        _player.playerMovement.ChangeRb2DMat(_player.noFrictionMat);
        //_player.ChangeState(new PlayerPushedState(_player));
    }
    public override void InterruptState()
    {
        base.InterruptState();
        _player.anim.SetAnimator(true);
    }
}
