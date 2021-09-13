using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MushroomManAttackState : EnemyState
{
    private bool _isAttacking;
    private bool _isIdle;
    private bool _isHit;
    private AnimationManager _anim;
    private MushroomGuyEnemy _enemy;
    private EnemyAudioManager _audio;
    public MushroomManAttackState(MushroomGuyEnemy enemy)
    {
        _audio = enemy.GetAudioManager();
        _enemy = enemy;
        _anim = _enemy.GetAnimationManager();

    }
    public override void Update()
    {
        if(!_isHit) Attack();
        //if (!_isAttacking && _isIdle) canChangeState = true;
        //else canChangeState = false;
    }
    private void Attack()
    {
        if (_isAttacking||_isIdle) return;
        _isAttacking = true;
        _anim.PlayAnimation("Attack");
        _audio.PlayAttackSound();
        _enemy.StartCoroutine(_enemy.WaitAndExecuteFunction(_anim.GetAnimationLength("Attack"), () =>
        {
            _anim.PlayAnimation("Idle");
            _isIdle = true;
            _isAttacking = false;
            _enemy.StartCoroutine(_enemy.WaitAndExecuteFunction(_anim.GetAnimationLength("Idle"), () => 
            {
                _isIdle = false; 
                if(!_enemy.IsPlayerInRange)
                {
                    _enemy.ReturnToPatrol();
                }
            }));
        }));
    }
    public override void SetUpState()
    {
        _isAttacking = false;
        _isIdle = false;
        canChangeState = false;
    }
    public override void Hit()
    {
        Debug.Log("Hit");
        _isHit = true;
        _isIdle = false;
        _isAttacking = false;
        _enemy.StopAllCoroutines();
        _anim.PlayAnimation("Hit");
        _enemy.StartCoroutine(_enemy.WaitAndExecuteFunction(_anim.GetAnimationLength("Hit"), () =>
         {
             if(!_enemy.IsPlayerInRange)
             {
                 _enemy.Rotate();
             }
             _isHit = false;
             _isIdle = true;
             _anim.PlayAnimation("Idle");
             _enemy.StartCoroutine(_enemy.WaitAndExecuteFunction(_anim.GetAnimationLength("Idle"), () =>
             {
                 _isIdle = false;
             }));
         }));
    }
}
