using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MushroomGuyEnemy : PatrollingEnemy
{
    public float attackDelay = 3f;
    private bool _attack;
    private bool _isAttacking;
    private Vector3[] patrolPos = new Vector3[2]; // first is left
    public Transform[] patrolTransofrms = new Transform[2];

    private bool _isPatrollingLeft = false;
    private bool _isPatrollingRight = true;
    private PlayerDetection _detection;

    private int lastPatrol=1;
    Coroutine cor;

    // Start is called before the first frame update
    void Start()
    {
        //_anim = GetComponent<AnimationManager>();
        //hpSys = transform.GetComponent<HealthSystem>();
        //hpSys.OnHitEvent += TakeDamage;
        //patrolPos[0] = patrolTransofrms[0].position;
        //patrolPos[1] = patrolTransofrms[1].position;
        SetUpBehaviour();
        SetUpComponents();
    }
    protected override void SetUpComponents()
    {
        base.SetUpComponents();
        _detection = GetComponentInChildren<PlayerDetection>();
        _detection.OnPlayerDetected = SetPlayerInRange;
        _detection.OnPlayerLeft = SetPlayerNotInRange;
        //hpSys.OnHit += Hit;
        //hpSys.OnDeath = Kill;
    }
    // Update is called once per frame
    void Update()
    {
        if (_isAlive)
        {
            if (!_isHit)
            {
                if (currentState == EnemyEnums.State.PATROLLING)
                {
                    _anim.PlayAnimation("Move");
                    MoveToPatrolPoint();
                }
                if (currentState == EnemyEnums.State.ALWAYS_IDLE)
                {
                    if (!_isIdle) StartCoroutine(StayIdleCor());
                }
                if (currentState == EnemyEnums.State.IDLE_AT_PATROL_POINT)
                {
                    if (!_isIdle) StayIdleAtPatrolPoint();
                }
                if (currentState == EnemyEnums.State.ATTACKING)
                {
                    Attack();
                }
                if (currentState == EnemyEnums.State.IDLE_AFTER_HIT)
                {
                    if (!_isIdle) StayIdleAfterHit();
                }
            }
        }
    }
    private void Attack()
    {
        if (_isAttacking) return;
        _isAttacking = true;
        _anim.PlayAnimation("Attack");
        RaiseOnAttackEvent();
        StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Attack"), () =>
        {
            _anim.PlayAnimation("Idle");
            StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Idle"), () => { _isAttacking = false; }));
        }));
    }
    private void StayIdleAfterHit()
    {
        StartCoroutine(StayIdleCor());
        StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Idle"), ResumeActions));
    }
    protected override void Kill()
    {
        IncreaseInvicibilityProgress();
        Destroy(this.gameObject);
    }

    public override void SetPlayerInRange()
    {
        if (currentState != EnemyEnums.State.DEAD)
        {
            states.Push(currentState);
            currentState = EnemyEnums.State.ATTACKING;
            StopCurrentActions();
            //Attack();
        }
    }

    public override void SetPlayerNotInRange()
    {
        if (currentState != EnemyEnums.State.DEAD)
        {
            StartCoroutine(WaitAndExecuteFunction(_anim.GetCurrentAnimationRemainingLength(), ResumeActions));
        }
    }
    public void TakeDamage()
    {
        _anim.PlayAnimation("Hit");
        if(!_attack)
        {
            Rotate();
        }
    }

    IEnumerator AttackDelayCor()
    {
        _isAttacking = true;
        _anim.PlayAnimation("Attack");
        RaiseOnAttackEvent();
        yield return new WaitForSeconds(attackDelay);
    }

    void Rotate()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
