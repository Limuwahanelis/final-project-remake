using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrystal : MonoBehaviour
{
    public float radius = 20;
    public float curAngle = 0;
    public float angleToMove = 5;
    public float fireDelay = 0.5f;
    
    public Vector3 positionToMove;
    public Vector3 positionToShootAt;
    public GameObject sprite;
    public GameObject beam;
    public Transform beamPos;

    public bool readyTofire = false;
    public bool endAttack = false;
    public bool isBackInPos = true;

    public float speed = 5f;

    private Vector3 _pos;

    private float _timetoEnd = 0.6f;
    private bool _isGoingBack = false;
    private bool _rotate = true;
    private bool _rotateToPos=false;
    private bool _moveToPos = false;
    private bool _rotateToTarget = false;



    //Coroutine cor;
    // Start is called before the first frame update
    void Start()
    {
        sprite.transform.localPosition = new Vector3(Mathf.Sin(Convert(curAngle)) * radius, Mathf.Cos(Convert(curAngle)) * radius);
        sprite.transform.up = sprite.transform.localPosition;
        beam.transform.position = beamPos.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        beam.transform.position = beamPos.position;
        if (_rotate)
        {
            curAngle += angleToMove * Time.deltaTime;
            _pos = new Vector3(Mathf.Sin(Convert(curAngle)) * radius, Mathf.Cos(Convert(curAngle)) * radius);
            sprite.transform.localPosition = _pos;
            sprite.transform.up = sprite.transform.localPosition;
            if (curAngle >= 360) curAngle = 0;
        }
        else
        {
            if (!_isGoingBack)
            {
                if (_rotateToPos)
                {
                    sprite.transform.up = positionToMove - sprite.transform.position;
                    _rotateToPos = false;
                }
                if (_moveToPos)
                {
                    if (Vector3.Distance(sprite.transform.position, positionToMove) > 0.001f)
                    {
                        float step = speed * Time.deltaTime;
                        sprite.transform.position = Vector3.MoveTowards(sprite.transform.position, positionToMove, step);
                    }
                    else
                    {
                        _moveToPos = false;
                        _rotateToTarget = true;
                    }
                }
                if (_rotateToTarget)
                {
                    sprite.transform.up = positionToShootAt - sprite.transform.position;
                    readyTofire = true;
                }
            }
            else
            {
                sprite.transform.up = _pos - sprite.transform.position;
                float step = speed * Time.deltaTime;
                if (Vector3.Distance(sprite.transform.localPosition,_pos) > 0.0001f)
                {
                    sprite.transform.localPosition = Vector3.MoveTowards(sprite.transform.localPosition, _pos, step);
                }
                else
                {
                    _isGoingBack = false;
                    sprite.transform.localPosition = _pos;
                    sprite.transform.up = sprite.transform.localPosition;
                    isBackInPos = true;
                }

            }
        }
    }

    private float Convert(float angleInDeg)
    {
        return Mathf.Deg2Rad * angleInDeg;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    IEnumerator Attack1Cor()
    {
        yield return new WaitForSeconds(fireDelay);
        beam.transform.up = sprite.transform.up;
        beam.SetActive(true);
        float time = 0;
        while (beam.transform.localScale.y < 25 && time<_timetoEnd)
        {
            beam.transform.localScale = new Vector3(beam.transform.localScale.x, beam.transform.localScale.y + 0.1f, 1);
            yield return new WaitForSeconds(0.008f);
            time += 0.008f;
        }
        beam.SetActive(false);
        endAttack = true;
    }
    public void SetRotate(bool val)
    {
        _rotate = val;
    }
    public void SetPostionToMove(Vector3 pos)
    {
        _rotate = false;
        positionToMove = pos;
        _moveToPos = true;
        _rotateToPos = true;
        readyTofire = false;
        endAttack = false;
        _rotateToTarget = false;
        isBackInPos = false;
        beam.transform.localScale = new Vector3(2, 1, 1);
        //if (cor != null) StopCoroutine(cor);
    }
    public void SetTarget(Vector3 target)
    {
        positionToShootAt = target;
    }
    public void FireBeam()
    {
        StartCoroutine(Attack1Cor());
    }
    public void MoveBack()
    {
        _isGoingBack = true;
    }
    public void SetAttackDuration(float attackTime)
    {
        _timetoEnd = attackTime;
    }
}
