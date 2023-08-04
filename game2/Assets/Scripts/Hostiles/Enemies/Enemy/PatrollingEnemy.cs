using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : Enemy
{

    public bool isMovingVertically;
    public int idleCycles;
    [SerializeField] protected List<Transform> _patrolPoints = new List<Transform>();
    protected List<Vector3> _patrolPositions = new List<Vector3>();

    public override void SetPlayerInRange()
    {
        throw new System.NotImplementedException();
    }

    public override void SetPlayerNotInRange()
    {
        throw new System.NotImplementedException();
    }
}
