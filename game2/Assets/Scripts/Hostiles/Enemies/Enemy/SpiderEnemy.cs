using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpiderEnemyCollisionInteractionComponent))]
public class SpiderEnemy : PatrollingEnemy
{
    [SerializeField]
    private SpiderEnemyCollisionInteractionComponent _collisionComponent;

    private EnemyPatrollingState _patrolState;

    // Start is called before the first frame update
    private void Awake()
    {
        SetUpComponents();
    }
    void Start()
    {
        if (patrolPoints.Count < 2)
        {
            Debug.LogError("Not enough patrol points");
            return;
        }
        _patrolState = new EnemyPatrollingState(this);
        state = _patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused.value)
        {
            _patrolState.Update();
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
