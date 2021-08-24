using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : Enemy,IDamagable
{
    public static Action OnGameCompleteEvent;

    public GameObject[] beams;
    public Player player;
    public Transform missileSpawn;
    public GameObject missilePrefab;
    public BossCrystalManager crystals;
    public Vector3 delayedBeamPos;
    public Transform attackTrans;
    public Transform vulnerableTrans;
    private Vector3 _attackPos;
    private Vector3 _vulnerablePos;
    //public GameObject credits;
    public float attackDelay = 1f;
    public float vulnerableTime = 2f;
    private int _attackPatten = 1;
    public bool attack = true;
    private bool _moveToVulnerablePos = false;
    private bool _moveToAttackPos= false;

    void Awake()
    {

        
    }
    // Start is called before the first frame update
    void Start()
    {
        _attackPos = attackTrans.position;
        _vulnerablePos = vulnerableTrans.position;
        hpSys = GetComponent<HealthSystem>();
    }

    void Update()
    {
        if (_isAlive)
        {
            if (attack)
            {
                switch (_attackPatten)
                {
                    case 1: StartCoroutine(AttackCor1()); break;
                    case 2: StartCoroutine(AttackCor2()); break;
                    case 3: StartCoroutine(AttackCor3()); break;
                    default: break;
                }
                attack = false;
                _attackPatten++;
                if (_attackPatten > 3) _attackPatten = 1;
            }
            if(_moveToVulnerablePos)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _vulnerablePos, step);
                if(Vector3.Distance(transform.position,_vulnerablePos)<0.001f)
                {
                    _moveToVulnerablePos = false;
                    StartCoroutine(VulnerableCor());
                }
            }
            if (_moveToAttackPos)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _attackPos, step);
                if (Vector3.Distance(transform.position, _attackPos) < 0.001f)
                {
                    _moveToAttackPos = false;
                    crystals.StartCrystalAttacks();
                }
            }
        }
        else
        {
            if (_moveToVulnerablePos)
            {
                
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _vulnerablePos, step);
                if (Vector3.Distance(transform.position, _vulnerablePos) < 0.001f)
                {
                    Kill();
                    _moveToVulnerablePos = false;
                }
            }
        }
    }

    IEnumerator AttackCor1()
    {
        yield return new WaitForSeconds(attackDelay);
        for(int i=0;i<10;i++)
        {
            beams[i].transform.position = new Vector3(player.transform.position.x, beams[i].transform.position.y);
            beams[i].GetComponent<DelayedBeam>().SetCor();
            if(i>3)
            {
                beams[i - 4].GetComponent<DelayedBeam>().DisableCor();
                beams[i - 4].transform.localPosition = delayedBeamPos;
            }
            yield return new WaitForSeconds(0.5f);
        }
        for(int i=6;i<10;i++)
        {
            beams[i].GetComponent<DelayedBeam>().DisableCor();
            beams[i].transform.localPosition = delayedBeamPos;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(attackDelay);
        Debug.Log("end attack");
        _moveToVulnerablePos = true;
    }
    IEnumerator AttackCor2()
    {
        yield return new WaitForSeconds(attackDelay);
        for (int i=0;i<60;i++)
        {
            GameObject missile =  Instantiate(missilePrefab, missileSpawn.transform.position, Quaternion.Euler(0,0,90+i*20));
            AudioSource tmpSource = missile.AddComponent<AudioSource>();
            GetComponent<BossAudioManager>().PlayAttackSound(tmpSource);
            missile.GetComponent<Missile>().SetSpeed(10);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(attackDelay);
        _moveToVulnerablePos = true;
    }
    IEnumerator AttackCor3()
    {
        yield return new WaitForSeconds(attackDelay);
        for (int i = 0; i < 20; i++)
        {
            GameObject missile = Instantiate(missilePrefab, missileSpawn.transform.position,missilePrefab.transform.rotation);
            missile.transform.up = player.transform.position - missileSpawn.transform.position;
            AudioSource tmpSource = missile.AddComponent<AudioSource>();
            GetComponent<BossAudioManager>().PlayAttackSound(tmpSource);
            missile.GetComponent<Missile>().SetSpeed(10);
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(attackDelay);
        _moveToVulnerablePos = true;
    }
    IEnumerator VulnerableCor()
    {
        yield return new WaitForSeconds(vulnerableTime);
        _moveToAttackPos = true;
    }
    public void StartAttacking()
    {
        attack = true;
    }
    public void TakeDamage(int dmg)
    {
        hpSys.TakeDamage(dmg);
        if(hpSys.currentHP<=0)
        {
            _isAlive = false;
            _moveToVulnerablePos = true;
        }
    }

    public void Kill()
    {
        _anim.PlayAnimation("Dead");
        StopAllCoroutines();
        crystals.DestroyCrystals();
    }
    public void ShowCredits()
    {
        OnGameCompleteEvent?.Invoke();
    }
    public void SetAttack()
    {
        attack = true;
    }
}
