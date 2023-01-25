using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropBombState : PlayerState
{
    public PlayerDropBombState(Player player) : base(player)
    {
        if (_player.isAttacking) return;
        _player.isAttacking = true;
        _player.anim.PlayAnimation("Drop Bomb");
        _player.StartCoroutine(_player.WaitAndExecuteFunction(_player.anim.GetAnimationLength("Drop Bomb"), () =>
        {
            _player.playerCombat.SpawnBomb();
            _player.ChangeState(new PlayerNormalState(player));
            _player.isAttacking = false;
        }));
    }

    public override void Update()
    {
    }

}
