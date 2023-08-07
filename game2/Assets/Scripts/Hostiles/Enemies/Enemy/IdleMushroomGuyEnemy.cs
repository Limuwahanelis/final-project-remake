using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IdleMushroomGuyEnemy : Enemy
{
    [SerializeField] PlayerDetection _playerDetectionFront;
    [SerializeField] PlayerDetection _playerDetectionBack;
    [SerializeField] private Beam beam;
    private PlayerDetection _playerDetection;
    private Action<bool> OnPlayerInRange;
    IdleMushroomGuyContext context;
    private void Awake()
    {

        SetUpComponents();
    }
    private void Start()
    {
        context = new IdleMushroomGuyContext()
        {
            ChangeState = ChangeState,
            Rotate = Rotate,
            audio = _audioMan,
            anim=_anim,
        };
        OnPlayerInRange += context.SetPlayerInRange;
        state = new IdleMushroomGuyIdleState(context);
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
    public void RotateTowardsPlayer()
    {
        context.SetPlayerBehind();
    }

    public override void SetPlayerInRange()
    {
        OnPlayerInRange?.Invoke(true);

    }

    public override void SetPlayerNotInRange()
    {
        OnPlayerInRange?.Invoke(false);
    }
    public void ChangeState(EnemyState newState)
    {
        Debug.Log(newState);
        state.InterruptState();
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
