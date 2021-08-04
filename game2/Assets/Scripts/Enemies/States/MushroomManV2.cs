using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MushroomManV2 : PatrollingEnemy
{
    private PlayerDetection _playerDetection;
    private EnemyState[] possibleStates = new EnemyState[Enum.GetValues(typeof(EnemyEnums.State)).Length];
    private EnemyEnums.State _currentStateIndex;
    private bool _isPlayerInRange;
    private bool _isChecking = false;
    private void Awake()
    {
        SetUpComponents();
        possibleStates[(int)EnemyEnums.State.ATTACKING] = new MushroomManAttackState(this, _anim);
        possibleStates[(int)EnemyEnums.State.PATROLLING] = new MushroomManPatrollingState(this);
        possibleStates[(int)EnemyEnums.State.ALWAYS_IDLE] = new MushroomManIdleState(this);
    }
    private void Start()
    {
        _currentStateIndex = EnemyEnums.State.PATROLLING;
        state = possibleStates[(int)EnemyEnums.State.PATROLLING];
    }
    private void Update()
    {
        state.Update();
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
        StartCoroutine(AddNewStateCor(EnemyEnums.State.ATTACKING));
    }

    public override void SetPlayerNotInRange()
    {
        _isPlayerInRange = false;
        StartCoroutine(CheckForPlayerReEnterCor());
    }
    protected override void AddNewState(EnemyEnums.State newState)
    {
        if (((int)newState) == ((int)_currentStateIndex)) return;

        state.SetUpState();
        states.Push(state);
        StopAllCoroutines();

        state = possibleStates[(int)newState];
        state.SetUpState();
        _currentStateIndex = newState;
    }
    protected override void ResumePreviousState()
    {
        state.SetUpState();
        state = states.Pop();
        StopAllCoroutines();
        state.SetUpState();
        _currentStateIndex = (EnemyEnums.State)GetStateIndex(state);
    }
    IEnumerator AddNewStateCor(EnemyEnums.State newState)
    {
        while(!state.CheckIfStateCanBeChanged())
        {
            yield return null;
        }
        AddNewState(newState);
    }
    private int GetStateIndex(EnemyState stateToFindIndex)
    {
        for(int i=0;i<possibleStates.Length;i++)
        {
            if (possibleStates[i] == stateToFindIndex) return i;
        }
        Debug.LogError("");
        return -1;
    }
    IEnumerator CheckForPlayerReEnterCor()
    {
        if (_isChecking) yield break;
        _isChecking = true;
        while (!state.CheckIfStateCanBeChanged())
        {
            if (_isPlayerInRange)
            {
                _isChecking = false;
                yield break;
            }
            yield return null;
        }
        ResumePreviousState();
        _isChecking = false;
    }
}
