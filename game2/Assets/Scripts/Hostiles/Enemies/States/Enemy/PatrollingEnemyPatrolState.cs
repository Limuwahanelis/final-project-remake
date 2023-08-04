using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyPatrolState : EnemyState
{
    protected List<Vector3> _patrolpositions = new List<Vector3>();
    protected Transform _enemy;
    protected int _patrolPointIndex = 0;
    protected PatrollingEnemyContext _context;
    public PatrollingEnemyPatrolState(PatrollingEnemyContext patrollingEnemyContext)
    {
        _context = patrollingEnemyContext;
        _enemy = patrollingEnemyContext.enemy;
        _patrolpositions = patrollingEnemyContext.patrolPoositons;
        SetUpState();
    }

    public override void Update()
    {
        MoveToPatrolPoint();
    }

    public void MoveToPatrolPoint()
    {

        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _patrolpositions[_patrolPointIndex], _context.speed * Time.deltaTime);

        if (_context.isMovingVertically)
        {
            if (Mathf.Abs(_enemy.transform.position.y - _patrolpositions[_patrolPointIndex].y) < 0.1)
            {
                if (_patrolPointIndex + 1 > _patrolpositions.Count - 1) _patrolPointIndex = 0;
                else _patrolPointIndex++;
                RotateTowardsPatrolPoint();
            }
        }
        else
        {
            if (Mathf.Abs(_enemy.transform.position.x - _patrolpositions[_patrolPointIndex].x) < 0.1)
            {
                if (_patrolPointIndex + 1 > _patrolpositions.Count - 1) _patrolPointIndex = 0;
                else _patrolPointIndex++;
                RotateTowardsPatrolPoint();
            }
        }
    }
    protected void RotateTowardsPatrolPoint()
    {
        float direction;

        if (_context.isMovingVertically)
        {
            if (_patrolpositions[_patrolPointIndex].y < _enemy.transform.position.y) direction = 1;
            else direction = -1;
        }
        else
        {
            if (_patrolpositions[_patrolPointIndex].x < _enemy.transform.position.x) direction = -1;
            else direction = 1;
        }
        _enemy.transform.localScale = new Vector3(direction, _enemy.transform.localScale.y, _enemy.transform.localScale.z);
    }


    protected void StayIdleAtPatrolPoint()
    {
        _context.ChangeState(new PatrollingEnemyIdleState(_context, this, _context.NumberOfIdleCycles));
    }
    public override void SetUpState()
    {
        RotateTowardsPatrolPoint();
        _context.anim.PlayAnimation("Move");
    }

}
