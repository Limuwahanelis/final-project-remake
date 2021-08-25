using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemy : Enemy
{

    public bool isMovingVertically;
    public int idleCycles;
    public List<Transform> patrolPoints = new List<Transform>();


    public override void SetPlayerInRange()
    {
        throw new System.NotImplementedException();
    }

    public override void SetPlayerNotInRange()
    {
        throw new System.NotImplementedException();
    }
}
