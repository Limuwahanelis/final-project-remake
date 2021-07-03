using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MushroomGuyEnemy : Enemy
{
    public float attackDelay = 3f;
    private bool _attack;
    private bool _isAttacking;
    private Vector3[] patrolPos = new Vector3[2]; // first is left
    public Transform[] patrolTransofrms = new Transform[2];

    private bool _isPatrollingLeft = false;
    private bool _isPatrollingRight = true;

    private int lastPatrol=1;
    Coroutine cor;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<AnimationManager>();
        hpSys = transform.GetComponent<HealthSystem>();
        hpSys.OnHitEvent += TakeDamage;
        patrolPos[0] = patrolTransofrms[0].position;
        patrolPos[1] = patrolTransofrms[1].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_attack)
        {
            if (!_isAttacking)
            {
                cor=StartCoroutine(AttackDelayCor());
            }
        }
        if (!_isAttacking)
        {
            if (_isPatrollingLeft)
            {
                MoveLeft();
            }
            if (_isPatrollingRight)
            {
                MoveRight();
            }
        }
    }
    private void MoveLeft()
    {
        _anim.PlayAnimation("Move");
        transform.position = Vector3.MoveTowards(transform.position, patrolPos[0], speed * Time.deltaTime);
        //RaiseOnWalkEvent();
        if (transform.position.x <= patrolPos[0].x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _isPatrollingRight = true;
            _isPatrollingLeft = false;
            lastPatrol = 1;
        }
    }
    private void MoveRight()
    {
        _anim.PlayAnimation("Move");
        transform.position = Vector3.MoveTowards(transform.position, patrolPos[1], speed * Time.deltaTime);
        //RaiseOnWalkEvent();
        if (transform.position.x >= patrolPos[1].x)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _isPatrollingRight = false;
            _isPatrollingLeft = true;
            lastPatrol = 0;
        }
    }
    public void Kill()
    {
        IncreaseInvicibilityProgress();
        Destroy(this.gameObject);
    }

    public override void SetPlayerInRange()
    {
        _attack = true;
        _isPatrollingLeft = false;
        _isPatrollingRight = false;
       _anim.PlayAnimation("Idle");
    }

    public override void SetPlayerNotInRange()
    {
        _attack = false;
        //isAttacking = true;
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
        yield return new WaitForSeconds(attackDelay);

        _anim.PlayAnimation("Attack");
        RaiseOnAttackEvent();
        
    }
    public void EndAttack()
    {
        _isAttacking = false;
        if (lastPatrol == 0) _isPatrollingLeft = true;
        else _isPatrollingRight = true;
    }

    void Rotate()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
