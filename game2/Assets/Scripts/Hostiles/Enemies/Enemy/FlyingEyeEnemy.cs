using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyeEnemy : Enemy
{

    public float radius = 20;
    public float angularVelocity = 5f;
    float curAngle = 0;
    public Transform sprite;
    public Transform mainBody;
    public GameObject missilePrefab;
    public int attacksInSeries = 3;

    private PlayerDetectionConstant _detection;
    private Vector3 _playerPos;
    
    private bool _isPlayerInRange = false;
    private bool _cooldown = false;

    private int _attackCount = 0;
    private void Awake()
    {
        SetUpComponents();
    }

    protected override void SetUpComponents()
    {
        base.SetUpComponents();
        _detection = GetComponentInChildren<PlayerDetectionConstant>();
        _detection.OnPlayerDetected = SetPlayerInRange;
        _detection.OnPlayerLeft = SetPlayerNotInRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused.value)
        {
            Move();
            if (_isPlayerInRange)
            {
                RotateTowardsPlayer();
                StartCoroutine(AttackCor());
            }
        }
    }
    IEnumerator AttackCor()
    {
        if (_cooldown) yield break;
        _cooldown = true;
        for (_attackCount = 0; _attackCount < attacksInSeries; _attackCount++)
        {
            GetAnimationManager().PlayAnimation("Attack");
            yield return new WaitForSeconds(_anim.GetAnimationLength("Attack") + 0.01f);
        }
        _anim.PlayAnimation("Idle");
        yield return new WaitForSeconds(3f);
        _cooldown = false;
    }
    public void SpawnMissile()
    {
        missilePrefab.transform.up = _playerPos - sprite.transform.position;
        _audioMan.PlayAttackSound();
        Instantiate(missilePrefab, sprite.transform.position, missilePrefab.transform.rotation);
    }

    private float Convert(float angleInDeg)
    {
        return Mathf.Deg2Rad * angleInDeg;
    }

    private void Move()
    {
        curAngle += angularVelocity * Time.deltaTime;
        mainBody.transform.localPosition = new Vector3(Mathf.Sin(Convert(curAngle)) * radius, Mathf.Cos(Convert(curAngle)) * radius);
        if (curAngle >= 360) curAngle = 0;
    }

    private void RotateTowardsPlayer()
    {
        _playerPos = _detection.playerPos;
        sprite.transform.right = _playerPos - sprite.transform.position;
        if (sprite.transform.right.x < 0) sprite.GetComponent<SpriteRenderer>().flipY = true;
        else sprite.GetComponent<SpriteRenderer>().flipY = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    public override void SetPlayerInRange()
    {
        _isPlayerInRange = true;
    }

    public override void SetPlayerNotInRange()
    {
        _isPlayerInRange = false;
        sprite.transform.right = new Vector2(1f, 0);
        sprite.GetComponent<SpriteRenderer>().flipY = false;
    }
}
