using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private Rigidbody2D _rb;
    public float speed;
    public float jumpStrength;
    public float airAttackSpeed;
    public GameObject toRotate;

    private int _flipSide = 1;
    //private bool _isMovableByPlayer = true;

    private float _previousDirection;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerIsFalling();
    }


    public void MovePlayer(float direction)
    {
        if (direction != 0)
        {
            if (_player.isMovableByPlayer)
            {
                _rb.velocity = new Vector3(direction * speed, _rb.velocity.y, 0);
                if (direction > 0)
                {
                    _flipSide = 1;
                    _player.mainBody.transform.localScale = new Vector3(_flipSide, _player.mainBody.transform.localScale.y, _player.mainBody.transform.localScale.z);
                }
                if (direction < 0)
                {
                    _flipSide = -1;
                    _player.mainBody.transform.localScale = new Vector3(_flipSide, _player.mainBody.transform.localScale.y, _player.mainBody.transform.localScale.z);
                }
                _player.isMoving = true;
            }
        }
        else
        {
            if (_previousDirection != 0) StopPlayerOnXAxis();
        }
        _previousDirection = direction;
    }
    public void StopPlayer()
    {
        _player.isMoving = false;
        _rb.velocity = new Vector2(0, 0);
    }
    public void StopPlayerOnXAxis()
    {
        _player.isMoving = false;
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }
    public void StopPlayerOnYAxis()
    {
        _player.isMoving = false;
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
    }
    public void MakePlayerIdle()
    {
        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        _player.isMoving = false;
    }
    public void Jump()
    {
        if (!_player.isJumping && _player.isOnGround)
        {
            _player.isJumping = true;
            _player.canPlayIdleAnim = false;
            _player.canPlayWalkAnim = false;
            _player.isMovableByPlayer = false;
            _player.TakeControlFromPlayer(Player.Cause.JUMP);
            _player.anim.PlayAnimation("Jump");
            StartCoroutine(JumpCor());
        }
    }
    public void JumpAnimationLogic()
    {

        _rb.velocity = new Vector3(0, 0, 0);
        _rb.AddForce(new Vector2(0, jumpStrength));
        _player.isJumping = false;
        _player.isAnimationPlaying = false;
    }
    private void CheckIfPlayerIsFalling()
    {
        if (_rb.velocity.y < 0) _player.isFalling = true;
        else _player.isFalling = false;
    }
    public void AirAttackAnimationLogic(float airAttackDuration)
    {
        _rb.gravityScale = 0;
        _rb.velocity = new Vector2(airAttackSpeed * _player.mainBody.transform.localScale.x, 0);
        StartCoroutine(AirAttackCor(airAttackDuration));
    }

    IEnumerator AirAttackCor(float airAttackDuration)
    {

        yield return new WaitForSeconds(airAttackDuration);
        _rb.velocity = new Vector2(0, 0);
        _rb.gravityScale = 2;
    }

    IEnumerator JumpCor()
    {
        while (_player.isOnGround) yield return null;
        _player.isJumping = false;
        _player.ReturnControlToPlayer(Player.Cause.JUMP);
    }
}
