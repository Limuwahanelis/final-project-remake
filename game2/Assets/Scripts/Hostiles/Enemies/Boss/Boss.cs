using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : Enemy
{
    public GameObject credits;
    public GameObject[] beams;
    new public BossAudioManager audio;
    public Player player;
    public Transform missileSpawn;
    public GameObject missilePrefab;
    public BossCrystalManager crystals;
    public Vector3 delayedBeamPos;
    public Transform attackTrans;
    public Transform vulnerableTrans;

    //public GameObject credits;
    public float attackDelay = 1f;
    public float vulnerableTime = 2f;
    [HideInInspector] public Vector3 attackPos;
    [HideInInspector] public Vector3 vulnerablePos;
    [HideInInspector] public int attackPatten = 1;

    private BossState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        attackPos = attackTrans.position;
        vulnerablePos = vulnerableTrans.position;
        _anim = GetComponent<AnimationManager>();
        hpSys = GetComponent<HealthSystem>();
        hpSys.OnDeathEvent = ()=> { 
            _isAlive = false;
            ChangeState(new BossMoveToVulnerablePosState(this,new BossDeadState(this),false));
        };
        ChangeState(new PlayerPosBeamAttackBosState(this));
    }

    void Update()
    {
        if (_isAlive)
        {
            _currentState.Update();
        }
    }
    public void PlayAnimation(string name)
    {
        _anim.PlayAnimation(name);
    }
    public void ChangeState(BossState newState)
    {
        _currentState = newState;
        _currentState.SetUpState();
    }
    public void ShowCredits()
    {
        credits.SetActive(true);
    }
}
