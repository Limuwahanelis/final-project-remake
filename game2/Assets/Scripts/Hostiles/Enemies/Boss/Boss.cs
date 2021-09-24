using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Boss : Enemy,IDamagable
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
    private Vector3 _attackPos;
    private Vector3 _vulnerablePos;
    private int _attackPatten = 1;
    private bool _attack = true;
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
        _anim = GetComponent<AnimationManager>();
        hpSys = GetComponent<HealthSystem>();
        hpSys.OnDeathEvent = ()=> { 
            _isAlive = false;
            _moveToVulnerablePos = true;
        };
    }

    void Update()
    {
        if (_isAlive)
        {
            if (_attack)
            {
                switch (_attackPatten)
                {
                    case 1: StartCoroutine(AttackCor1()); break;
                    case 2: StartCoroutine(AttackCor2()); break;
                    case 3: StartCoroutine(AttackCor3()); break;
                    default: break;
                }
                _attack = false;
                _attackPatten++;
                if (_attackPatten > 3) _attackPatten = 1;
            }
            if(_moveToVulnerablePos)
            {
                if(!MoveToPosition(_vulnerablePos))
                {
                    _moveToVulnerablePos = false;
                    StartCoroutine(VulnerableCor());
                }
            }
            if (_moveToAttackPos)
            {
                if(!MoveToPosition(_attackPos))
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
                if(!MoveToPosition(_vulnerablePos))
                {
                    Kill();
                    _moveToVulnerablePos = false;
                }
            }
        }
    }

    private bool MoveToPosition(Vector3 pos)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pos, step);
        if (Vector3.Distance(transform.position, pos) < 0.001f)
        {
            return false;
        }
        return true;
    }

    IEnumerator AttackCor1()
    {
        yield return new WaitForSeconds(attackDelay);
        for(int i=0;i<10;i++)
        {
            beams[i].transform.position = new Vector3(player.transform.position.x, beams[i].transform.position.y);
            audio.PlayBeamAudio(beams[i].GetComponent<AudioSource>());
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
        _attack = true;
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
        crystals.DestroyCrystals();
        _anim.PlayAnimation("Dead");
        StopAllCoroutines();
        
    }
    public void ShowCredits()
    {
        credits.SetActive(true);
    }
    public void SetAttack()
    {
        _attack = true;
    }
}
