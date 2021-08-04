using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MushroomGuyEnemy : PatrollingEnemy
{
    public float attackDelay = 3f;
    private bool _isAttacking;

    private PlayerDetection _detection;

    // Start is called before the first frame update
    void Start()
    {
        SetUpComponents();
    }
    protected override void SetUpComponents()
    {
        base.SetUpComponents();
        _detection = GetComponentInChildren<PlayerDetection>();
        _detection.OnPlayerDetected = SetPlayerInRange;
        _detection.OnPlayerLeft = SetPlayerNotInRange;
        //hpSys.OnHitEvent += Hit;
        //hpSys.OnDeath = Kill;
    }
    // Update is called once per frame
    void Update()
    {
        //if (_isAlive)
        //{
        //    if (!_isHit)
        //    {
        //        if (currentState == EnemyEnums.State.PATROLLING)
        //        {
        //            _anim.PlayAnimation("Move");
        //        }
        //        if (currentState == EnemyEnums.State.ALWAYS_IDLE)
        //        {
        //            if (!_isIdle) StartCoroutine(StayIdleCor());
        //        }
        //        if (currentState == EnemyEnums.State.IDLE_AT_PATROL_POINT)
        //        {
        //            if (!_isIdle) StayIdleAtPatrolPoint();
        //        }
        //        if (currentState == EnemyEnums.State.ATTACKING)
        //        {
        //            Attack();
        //        }
        //        if (currentState == EnemyEnums.State.IDLE_AFTER_HIT)
        //        {
        //            if (!_isIdle) StayIdleAfterHit();
        //        }
        //    }
        //}
    }
    //private void Attack()
    //{
    //    if (_isAttacking) return;
    //    _isAttacking = true;
    //    _anim.PlayAnimation("Attack");
    //    RaiseOnAttackEvent();
    //    StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Attack"), () =>
    //    {
    //        _anim.PlayAnimation("Idle");
    //        StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Idle"), () => { _isAttacking = false; }));
    //    }));
    //}
    //private void StayIdleAfterHit()
    //{
    //    StartCoroutine(StayIdleCor());
    //    StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Idle"), ResumeActions));
    //}
    //protected override void Kill()
    //{
    //    IncreaseInvicibilityProgress();
    //    Destroy(this.gameObject);
    //}

    //public override void SetPlayerInRange()
    //{
    //    if (currentState != EnemyEnums.State.DEAD)
    //    {
    //        AddState(currentState);
    //        currentState = EnemyEnums.State.ATTACKING;
    //        StopCurrentActions();
    //        //Attack();
    //    }
    //}

    //public override void SetPlayerNotInRange()
    //{
    //    if (currentState != EnemyEnums.State.DEAD)
    //    {
    //        StartCoroutine(WaitAndExecuteFunction(_anim.GetCurrentAnimationRemainingLength(), ResumeActions));
    //    }
    //}
    //protected override void StopCurrentActions()
    //{
    //    base.StopCurrentActions();
    //    _isIdle = false;
    //    _isAttacking = false;
    //}
    //protected override void ResumeActions()
    //{
    //    base.ResumeActions();
    //    _isAttacking = false;
    //    _isIdle = false;
    //}
    //void AddState(EnemyEnums.State newState)
    //{
    //    if (states.Count > 0)
    //    {
    //        if (states.Peek() == newState) return;
    //        states.Push(newState);
    //    }
    //    else states.Push(newState);
    //}
    //void Rotate()
    //{
    //    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //}
}
