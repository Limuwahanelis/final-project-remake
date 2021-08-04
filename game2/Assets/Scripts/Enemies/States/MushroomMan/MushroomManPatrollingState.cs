using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManPatrollingState : EnemyState
{
    PatrollingEnemy patrollingEnemy;
    private List<Vector3> _patrolpositions = new List<Vector3>();
    private int _patrolPointIndex = 0;
    public MushroomManPatrollingState(PatrollingEnemy enemy)
    {
        this.patrollingEnemy = enemy;
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
        
        patrollingEnemy.transform.position = Vector3.MoveTowards(patrollingEnemy.transform.position, _patrolpositions[_patrolPointIndex], patrollingEnemy.speed * Time.deltaTime);

        if (Mathf.Abs(patrollingEnemy.transform.position.x - _patrolpositions[_patrolPointIndex].x) < 0.1)
        {
            if (_patrolPointIndex + 1 > _patrolpositions.Count - 1) _patrolPointIndex = 0;
            else _patrolPointIndex++;
            RotateTowardsNextPoint();
        }
    }

    private void RotateTowardsNextPoint()
    {
        float direction;

        if (_patrolpositions[_patrolPointIndex].x <patrollingEnemy.transform.position.x) direction = -1;
        else direction = 1;

        patrollingEnemy.transform.localScale = new Vector3(direction, patrollingEnemy.transform.localScale.y, patrollingEnemy.transform.localScale.z);
    }

    public override void SetUpState()
    {
        canChangeState = true;
        RotateTowardsNextPoint();
        patrollingEnemy.GetAnimationManager().PlayAnimation("Move");
    }

}
