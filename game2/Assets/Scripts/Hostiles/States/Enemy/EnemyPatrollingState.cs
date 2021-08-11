using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingState : EnemyState
{
    private List<Vector3> _patrolpositions = new List<Vector3>();
    private int _patrolPointIndex = 0;
    private AnimationManager _anim;
    private PatrollingEnemy _enemy;
    public EnemyPatrollingState(PatrollingEnemy patrollingEnemy)
    {
        _anim = patrollingEnemy.GetAnimationManager();
        _enemy = patrollingEnemy;
        for (int i = 0; i < patrollingEnemy.patrolPoints.Count; i++)
        {
            _patrolpositions.Add(patrollingEnemy.patrolPoints[i].position);
        }
        SetUpState();
    }

    public override void Update()
    {
        MoveToPatrolPoint();
    }

    public void MoveToPatrolPoint()
    {

        _enemy.transform.position = Vector3.MoveTowards(_enemy.transform.position, _patrolpositions[_patrolPointIndex], _enemy.speed * Time.deltaTime);

        if (Mathf.Abs(_enemy.transform.position.x - _patrolpositions[_patrolPointIndex].x) < 0.1)
        {
            if (_patrolPointIndex + 1 > _patrolpositions.Count - 1) _patrolPointIndex = 0;
            else _patrolPointIndex++;
            RotateTowardsPatrolPoint();
        }
    }
    protected void RotateTowardsPatrolPoint()
    {
        float direction;

        if (_patrolpositions[_patrolPointIndex].x < _enemy.transform.position.x) direction = -1;
        else direction = 1;

        _enemy.transform.localScale = new Vector3(direction, _enemy.transform.localScale.y, _enemy.transform.localScale.z);
    }


    protected void StayIdleAtPatrolPoint()
    {
        _enemy.StartCoroutine(_enemy.StayIdleCor(_enemy.idleCycles));
        _enemy.StartCoroutine(_enemy.WaitAndExecuteFunction(_anim.GetAnimationLength("Idle") * _enemy.idleCycles, RotateTowardsPatrolPoint));
    }
    public override void SetUpState()
    {
        canChangeState = true;
        RotateTowardsPatrolPoint();
        _enemy.GetAnimationManager().PlayAnimation("Move");
    }

}
