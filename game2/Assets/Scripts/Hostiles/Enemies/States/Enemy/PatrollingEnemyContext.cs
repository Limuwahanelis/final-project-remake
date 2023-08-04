using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyContext : EnemyContext
{
    public PatrollingEnemyContext(int numberOfIdleCycles)
    {
        _numberOfIdleCycles = numberOfIdleCycles;
    }
    
    public List<Vector3> patrolPoositons = new List<Vector3>();
    public int patrolPointIndex = 0;
    public AnimationManager anim;
    public Transform enemy;
    public float speed;
    public bool isMovingVertically;
    public int NumberOfIdleCycles => _numberOfIdleCycles;
    private int _numberOfIdleCycles;
}
