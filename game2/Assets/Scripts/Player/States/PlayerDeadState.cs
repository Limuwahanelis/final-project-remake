using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player player) : base(player)
    {
        _player.anim.SetAnimator(true);
        player.playerMovement.StopPlayer();
        player.playerMovement.ChangeRb2DMat(null);
        //_player.normalColliders.SetActive(false);
        player.anim.PlayAnimation("Dead");
        player.isAlive = false;
    }
    public override void Update()
    {

    }
}
