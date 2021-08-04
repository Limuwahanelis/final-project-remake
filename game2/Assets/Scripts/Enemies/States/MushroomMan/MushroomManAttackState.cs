using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MushroomManAttackState : EnemyState
{
    private bool _isAttacking;
    private bool _isIdle;
    private AnimationManager _anim;
    public MushroomManAttackState(Enemy enemy,AnimationManager animationManager)
    {
        this.enemy = enemy;
        _anim = animationManager;
    }
    public override void Update()
    {
        Attack();
        if (!_isAttacking && _isIdle) canChangeState = true;
        else canChangeState = false;
    }
    private void Attack()
    {
        if (_isAttacking || _isIdle) return;
        _isAttacking = true;
        _anim.PlayAnimation("Attack");
        enemy.RaiseOnAttackEvent();
        enemy.StartCoroutine(enemy.WaitAndExecuteFunction(_anim.GetAnimationLength("Attack"), () =>
        {
            _anim.PlayAnimation("Idle");
            _isIdle = true;
            _isAttacking = false;
            enemy.StartCoroutine(enemy.WaitAndExecuteFunction(_anim.GetAnimationLength("Idle"), () => { _isIdle = false; }));
        }));
    }
    public override void SetUpState()
    {
        _isAttacking = false;
        _isIdle = false;
        canChangeState = false;
    }
}
