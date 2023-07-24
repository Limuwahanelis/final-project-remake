using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropBombState : PlayerState
{
    public PlayerDropBombState(PlayerContext playerContext) : base(playerContext)
    {
        _playerContext.anim.PlayAnimation("Drop Bomb");
        _playerContext.corutineHolder.StartCoroutine(_playerContext.WaitAndExecuteFunction(_playerContext.anim.GetAnimationLength("Drop Bomb"), () =>
        {
            _playerContext.playerCombat.SpawnBomb();
            _playerContext.ChangeState(new PlayerNormalState(playerContext));
        }));
    }

    public override void Update()
    {
    }

}
