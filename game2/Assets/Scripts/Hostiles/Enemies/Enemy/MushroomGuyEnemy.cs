using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MushroomGuyEnemy : PatrollingEnemy
{
    private PlayerDetection _playerDetection;
    private bool _isPlayerInRange;
    public bool IsPlayerInRange
    {
        get { return _isPlayerInRange; } 
    }
    private MushroomGuyPatrollingState _patrolState;
    private void Awake()
    {

        SetUpComponents();
    }
    private void Start()
    {
        if (patrolPoints.Count < 2)
        {
            Debug.LogError("fsaf");
            return;
        }
        _patrolState = new MushroomGuyPatrollingState(this);
        state = _patrolState;
        hpSys.OnHitEvent = Hit;
    }
    private void Update()
    {
        if (!isGamePaused.value)
        {
            state.Update();
        }
    }
    protected override void SetUpComponents()
    {
        base.SetUpComponents();
        _playerDetection = GetComponentInChildren<PlayerDetection>();
        _playerDetection.OnPlayerDetected = SetPlayerInRange;
        _playerDetection.OnPlayerLeft = SetPlayerNotInRange;
        
    }
    public override void SetPlayerInRange()
    {
        _isPlayerInRange = true;

    }

    public override void SetPlayerNotInRange()
    {
        _isPlayerInRange = false;
    }
    public void ReturnToPatrol()
    {
        state = _patrolState;
        _anim.PlayAnimation("Move");
    }
    public void ChangeState(EnemyState newState)
    {
        state = newState;
        state.SetUpState();
    }
    public EnemyAudioManager GetAudioManager()
    {
        return _audioMan;
    }

    private void Hit()
    {
        state.Hit();
    }
}
