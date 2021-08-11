using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
        else
        {
            if (_previousDirection != 0) StopPlayerOnXAxis();
        }
        _previousDirection = direction;
    }
    public void StopPlayer()
    {
        _rb.velocity = new Vector2(0, 0);
    }
    public void StopPlayerOnXAxis()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }
    public void StopPlayerOnYAxis()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
    }
    public void MakePlayerIdle()
    {
        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
    }
    public void Jump()
    {
        _player.isJumping = true;
        _player.anim.PlayAnimation("Jump");
    }
    public void JumpAnimationLogic()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        _rb.AddForce(new Vector2(0, jumpStrength));
        _player.isJumping = false;
    }
    public bool CheckIfPlayerIsFalling()
    {
        if (_rb.velocity.y < 0) return true;
        else return false;
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
    public IEnumerator JumpCor()
    {
        while (_player.isOnGround) yield return null;
        _player.isJumping = false;
        _player.ChangeState(new PlayerInAirState(_player));
    }
}
