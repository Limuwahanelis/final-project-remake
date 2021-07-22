using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeEnemy : Enemy,IDamagable
{
    public float radius = 20;
    float curAngle = 0;
    public float angleToMove=5;
    public Transform sprite;
    public Transform mainBody;
    //private GameManager gamMan;
    public GameObject missilePrefab;
    private PlayerDetection _detection;
    private Vector3 _playerPos;
    private bool _startAttacking=false;
    private int _attackCount = 0;
    private int _attacksInSeries = 3;
    private bool _cooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        //anim = transform.GetComponent<Animator>();
        //gamMan = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        SetUpComponents();

    }
    protected override void SetUpComponents()
    {
        base.SetUpComponents();
        _detection = GetComponentInChildren<PlayerDetection>();
        _detection.OnPlayerDetected = SetPlayerInRange;
        _detection.OnPlayerLeft = SetPlayerNotInRange;
    }
    // Update is called once per frame
    void Update()
    {
        curAngle += angleToMove*Time.deltaTime;
        mainBody.transform.localPosition = new Vector3( Mathf.Sin(Convert( curAngle))* radius, Mathf.Cos(Convert(curAngle))* radius);
        if (curAngle >= 360) curAngle = 0;

        if (currentState==EnemyEnums.State.ATTACKING)
        {
            _playerPos = _detection.playerPos;
            sprite.transform.right = _playerPos - sprite.transform.position;
            if (sprite.transform.right.x < 0) sprite.GetComponent<SpriteRenderer>().flipY = true;
            else sprite.GetComponent<SpriteRenderer>().flipY = false;

            if (currentState==EnemyEnums.State.ATTACKING)
            {
                StartCoroutine(AttackCor());
            }
        }
        else
        {
            _anim.PlayAnimation("Idle");       
        }
        //Debug.Log(sprite.transform.right);
    }
    private float Convert(float angleInDeg)
    {
        return Mathf.Deg2Rad * angleInDeg;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    IEnumerator AttackCor()
    {
        if (_cooldown) yield break;
        _cooldown = true;
        for ( _attackCount = 0; _attackCount < _attacksInSeries; _attackCount++)
        {
            _anim.PlayAnimation("Attack");
            yield return new WaitForSeconds(_anim.GetAnimationLength("Attack")+0.01f);
        }
        _anim.PlayAnimation("Idle");
        yield return new WaitForSeconds(3f);
        _cooldown = false;
        
    }
    public void SpawnMissile()
    {
        missilePrefab.transform.up = _playerPos - sprite.transform.position;
        RaiseOnAttackEvent();
        Instantiate(missilePrefab, sprite.transform.position, missilePrefab.transform.rotation);
        
    }
    //public void Attack()
    //{
    //    missilePrefab.transform.up =_playerPos - sprite.transform.position;
    //    RaiseOnAttackEvent();
    //    Instantiate(missilePrefab, sprite.transform.position, missilePrefab.transform.rotation);
    //    _attackCount++;
    //    if(_attackCount==_attacksInSeries)
    //    {
    //        _attackCount = 0;
    //        _anim.PlayAnimation("Attack",false);
    //        StartCoroutine(AttackCor());
    //    }
        
    //}

    public override void SetPlayerInRange()
    {
        if (currentState != EnemyEnums.State.DEAD)
        {
            states.Push(currentState);
            currentState = EnemyEnums.State.ATTACKING;
            StopCurrentActions();
            //Attack();
        }
        //_playerInRange = true;
    }
    public override void SetPlayerNotInRange()
    {
        if (currentState != EnemyEnums.State.DEAD)
        {
            //ResumeActions();
            currentState = EnemyEnums.State.ALWAYS_IDLE;
            sprite.transform.right = new Vector2(1f, 0);
            sprite.GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    public void TakeDamage(int dmg)
    {
        hpSys.TakeDamage(dmg);
        if(hpSys.currentHP.value<=0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        IncreaseInvicibilityProgress();
        Destroy(transform.gameObject);
    }

    public void Knockback()
    {
        throw new System.NotImplementedException();
    }

    public void SlowDown(float slowDownFactorx, float slowDownFactory)
    {
        throw new System.NotImplementedException();
    }
}
