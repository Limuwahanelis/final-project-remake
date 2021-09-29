using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MushroomGuyEnemy : PatrollingEnemy
{
    public bool IsPlayerInRange{ get { return _isPlayerInRange; } }
    [SerializeField]
    private Beam beam;
    private PlayerDetection _playerDetection;
    private bool _isPlayerInRange;
    private MushroomGuyPatrollingState _patrolState;
    
    private void Awake()
    {

        SetUpComponents();
    }
    private void Start()
    {
        if (patrolPoints.Count < 2)
        {
            Debug.LogError("Not enough patrol points");
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
        state.SetUpState();
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

    public void Rotate()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void Hit()
    {
        state.Hit();
    }

    private void OnValidate()
    {
        if (beam != null) beam.damage = dmg;
    }

}
