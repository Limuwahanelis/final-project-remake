using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(PlayerContext playerContext) : base(playerContext)
    {
        playerContext.anim.SetAnimator(true);
        playerContext.playerMovement.StopPlayer();
        playerContext.playerMovement.ChangeRb2DMat(null);
        //_player.normalColliders.SetActive(false);
        playerContext.anim.PlayAnimation("Dead");
        //playerContext.isAlive = false;
    }
    public override void Update()
    {

    }
}
