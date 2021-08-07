using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerState
{
    private bool _isJumping;
    public PlayerJumpingState(PlayerV2 player):base(player)
    { }
    public override void Update()
    {
        
    }
    public override void Jump()
    {
        if (_isJumping) return;
        _player.playerMovement.Jump();
        _player.StartCoroutine(_player.playerMovement.JumpCor());
        _isJumping = true;
    }


}
