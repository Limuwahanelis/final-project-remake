using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : Enemy
{


    public int idleCycles;
    public List<Transform> patrolPoints = new List<Transform>();
    private List<Vector3> _patrolpositions = new List<Vector3>();
    private int _patrolPointIndex = 0;


    protected void RotateEnemyTowardsNextPatrolPoint()
    {
        float direction;

        if (_patrolpositions[_patrolPointIndex].x < transform.position.x) direction = -1;
        else direction = 1;

        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
    }

    protected void StayIdleAtPatrolPoint()
    {
        StartCoroutine(StayIdleCor(idleCycles));
        StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Idle") * idleCycles, ResumePatrol));
    }

    protected void ResumePatrol()
    {
        //currentState = EnemyEnums.State.PATROLLING;
        RotateEnemyTowardsNextPatrolPoint();
    }

    public override void SetPlayerInRange()
    {
        throw new System.NotImplementedException();
    }

    public override void SetPlayerNotInRange()
    {
        throw new System.NotImplementedException();
    }
}
