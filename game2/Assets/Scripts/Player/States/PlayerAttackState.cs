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
        if (_player.isAttacking) return;
        _player.isAttacking = true;
        _player.audioManager.PlayNormalAttackSound();
        _player.isAttacking = true;
        _player.playerCombat.Attack(this);
    }

    public override void AttackIsOver()
    {
        _player.isAttacking = false;
        Debug.Log("End attack to normal");
        _player.ChangeState(new PlayerNormalState(_player));
    }
    public override void OnHit()
    {
        base.OnHit();
        Debug.Log("hit in hit");
        _player.isAttacking = false;
        _player.playerCombat.StopAttack();

    }
}
