using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private float _timer = 0;
    private float _attackAnimDuration;
    private Coroutine _attackCoroutine;
    public PlayerAttackState(PlayerContext playerContext) : base(playerContext)
    {
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer < _attackAnimDuration) return;
        _playerContext.ChangeState(new PlayerNormalState(_playerContext));
    }
    public override void SetUpState()
    {
        _attackAnimDuration = _playerContext.anim.GetAnimationLength("Attack1");
        _playerContext.playerMovement.StopPlayer();
        _playerContext.audioManager.PlayNormalAttackSound();
        _playerContext.anim.PlayAnimation("Attack1");
        _attackCoroutine = _playerContext.corutineHolder.StartCoroutine(_playerContext.playerCombat.AttackCor());
    }
    public override void OnHit()
    {
        base.OnHit();
        Debug.Log("hit in hit");
        _playerContext.playerCombat.StopAttack();

    }
    public override void InterruptState()
    {
        _playerContext.corutineHolder.StopCoroutine(_attackCoroutine);
    }
}
