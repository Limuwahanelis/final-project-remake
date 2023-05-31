using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : Enemy
{
    [SerializeField] GameObject _credits;
    [SerializeField] BossAudioManager _bossAudio;
    [SerializeField] Transform _playerTrans;
    [SerializeField] GameObject[] _beams;
    [SerializeField] Transform _missileSpawn;
    [SerializeField] GameObject _missilePrefab;
    [SerializeField] BossCrystalManager _crystals;
    [SerializeField] Transform _attackTrans;
    [SerializeField] Transform _vulnerableTrans;
    [SerializeField] Vector3 _delayedBeamPos;
    [SerializeField] float _vulnerableTime = 2f;

    private float _attackDelay = 1f;

    private BossContext _bossContext;
    private BossState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        _isAlive = false;
        Invoke("SetUp", 1f);

    }
    private void SetUp()
    {
        _bossContext = new BossContext()
        {
            attackPatten = 1,
            vulnerableTime = _vulnerableTime,
            bossAudio = _bossAudio,
            crystals = _crystals,
            missilePrefab = _missilePrefab,
            missileSpawnPos = _missileSpawn.position,
            speed = speed,
            attackDelay = _attackDelay,
            attackPos = _attackTrans.position,
            vulnerablePos = _vulnerableTrans.position,
            beams = _beams,
            delayedBeamPos = _delayedBeamPos,
            playerTrans = _playerTrans
        };

        _anim = GetComponent<AnimationManager>();
        hpSys = GetComponent<HealthSystem>();
        hpSys.OnDeathEvent = () => {
            ChangeState(new BossMoveToVulnerablePosState(this, new BossDeadState(this, _bossContext), _bossContext, false));
        };
        _isAlive = true;
        ChangeState(new PlayerPosBeamAttackBosState(this, _bossContext));
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
        Debug.Log(newState.GetType());
        _currentState = newState;
        _currentState.SetUpState();
    }
    public void ShowCredits()
    {
        _credits.SetActive(true);
    }
}
