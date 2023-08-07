using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MushroomGuyEnemy : PatrollingEnemy
{
    [SerializeField] private Beam beam;
    private PlayerDetection _playerDetection;
    private MushroomGuyPatrollingState _patrolState;
    private Action<bool> OnPlayerInRange;
    MushroomGuyContext context;
    private void Awake()
    {

        SetUpComponents();
    }
    private void Start()
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
        context = new MushroomGuyContext(idleCycles)
        {
            patrolPoositons = _patrolPositions,
            patrolPointIndex = 0,
            anim = _anim,
            enemy = transform,
            speed = _speed,
            isMovingVertically = isMovingVertically,
            OnSetPlayerInRange = OnPlayerInRange,
            ChangeState = ChangeState,
            Rotate = Rotate,
            audio = _audioMan
        };
        OnPlayerInRange += context.SetPlayerInRange;
        _patrolState = new MushroomGuyPatrollingState(context);
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
        OnPlayerInRange?.Invoke(true);

    }

    public override void SetPlayerNotInRange()
    {
        OnPlayerInRange?.Invoke(false);
    }
    public void ReturnToPatrol()
    {
        state = _patrolState;
        state.SetUpState();
        _anim.PlayAnimation("Move");
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
    private void Death()
    {

    }
    private void OnValidate()
    {
        if (beam != null) beam.damage = dmg;
    }
    private void OnDisable()
    {
        OnPlayerInRange -= context.SetPlayerInRange;
    }

}
