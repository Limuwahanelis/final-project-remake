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
    private bool _isChecking = false;
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
        _patrolState= new MushroomGuyPatrollingState(this);
        state = _patrolState;
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
        //StartCoroutine(CheckForPlayerReEnterCor());
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
    //IEnumerator CheckForPlayerReEnterCor()
    //{
    //    if (_isChecking) yield break;
    //    _isChecking = true;
    //    while (!state.CheckIfStateCanBeChanged())
    //    {
    //        if (_isPlayerInRange)
    //        {
    //            _isChecking = false;
    //            yield break;
    //        }
    //        yield return null;
    //    }
    //    state = _patrolState;
    //    _patrolState.SetUpState();
    //    _isChecking = false;
    //}
}
