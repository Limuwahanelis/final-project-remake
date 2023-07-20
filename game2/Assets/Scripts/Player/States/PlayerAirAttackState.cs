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
        _animLength = _playerContext.anim.GetAnimationLength("Air Attack");
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _animLength) return;
        _playerContext.ChangeState(new PlayerInAirState(_playerContext));
    }

    public override void SetUpState()
    {
        _playerContext.audioManager.PlayAirAttackSound();
        _playerContext.playerMovement.StartAirAttack();
        _playerContext.anim.PlayAnimation("Air attack");
        _playerContext.canPerformAirAttack = false;
    }

    public override void InterruptState()
    {
     
    }
    public void AirAttack()
    {
        _playerContext.canPerformAirAttack = false;
        //_player.isAirAttacking = true;

        //_airAttackCor = StartCoroutine(_player.playerMovement.AirAttackCor(_player.anim.GetAnimationLength("Air attack")));
        _airAttackCor = _playerContext.corutineHolder.StartCoroutine(_playerContext.playerCombat.AirAttackCor());
    }

}