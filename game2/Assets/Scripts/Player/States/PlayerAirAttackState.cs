using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerState
{
    private Coroutine _airAttackCor;
    private float _timer=0;
    float _animLength;
    public PlayerAirAttackState(PlayerContext context) : base(context)
    {
        _animLength = _playerContext.anim.GetAnimationLength("Air attack");
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _animLength) return;
        _playerContext.playerMovement.StopAirAttack();
        _playerContext.ChangeState(new PlayerInAirState(_playerContext));
    }

    public override void SetUpState()
    {
        _playerContext.audioManager.PlayAirAttackSound();
        _playerContext.playerMovement.StartAirAttack();
        _playerContext.anim.PlayAnimation("Air attack");
        Physics2D.IgnoreLayerCollision(9, 12, true);
        _playerContext.playerHealthSystem.SetInvincibility(PlayerHealthSystem.DamageType.ENEMY);
        _playerContext.playerHealthSystem.SetPushInvincibility(PlayerHealthSystem.DamageType.ENEMY);
        _airAttackCor = _playerContext.corutineHolder.StartCoroutine(_playerContext.playerCombat.AirAttackCor());
        _playerContext.canPerformAirAttack = false;
    }

    public override void InterruptState()
    {
        Physics2D.IgnoreLayerCollision(9, 12, false);
        _playerContext.playerHealthSystem.SetInvincibility(PlayerHealthSystem.DamageType.NONE);
        _playerContext.playerHealthSystem.SetPushInvincibility(PlayerHealthSystem.DamageType.NONE);
        _playerContext.playerMovement.StopAirAttack();
        _playerContext.corutineHolder.StopCoroutine(_airAttackCor);
    }

}