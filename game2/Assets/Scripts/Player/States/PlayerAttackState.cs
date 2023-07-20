using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerContext playerContext) : base(playerContext)
    {
    }

    public override void Update()
    {
    }
    public override void SetUpState()
    {
        
        Attack();
    }
    public override void Attack()
    {
        _playerContext.audioManager.PlayNormalAttackSound();
        _playerContext.playerCombat.Attack(this);
    }

    public override void AttackIsOver()
    {
        Debug.Log("End attack to normal");
        _playerContext.ChangeState(new PlayerNormalState(_playerContext));
    }
    public override void OnHit()
    {
        base.OnHit();
        Debug.Log("hit in hit");
        _playerContext.playerCombat.StopAttack();

    }
}
