using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpiderEnemyCollisionInteractionComponent))]
public class SpiderEnemy : PatrollingEnemy
{
    [SerializeField]
    private SpiderEnemyCollisionInteractionComponent _collisionComponent;

    private PatrollingEnemyPatrolState _patrolState;

    // Start is called before the first frame update
    private void Awake()
    {
        SetUpComponents();
    }
    void Start()
    {
        for (int i = 0; i < _patrolPoints.Count; i++)
        {
            _patrolPositions.Add(_patrolPoints[i].position);
        }
        if (_patrolPoints.Count < 2)
        {
            Debug.LogError("Not enough patrol points");
            return;
        }
        SpiderContext context = new SpiderContext(idleCycles)
        {
            patrolPoositons = _patrolPositions,
            patrolPointIndex = 0,
            anim = _anim,
            enemy = transform,
            speed = _speed,
            isMovingVertically = isMovingVertically,
            ChangeState = ChangeState,
        };
        _patrolState = new PatrollingEnemyPatrolState(context);
        state = _patrolState;
        state.SetUpState();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused.value)
        {
            state.Update();
        }
    }
    private void OnValidate()
    {
        if(_collisionComponent!=null)
        {
            _collisionComponent.SetCollisionDamage(dmg);
        }
    }
}
