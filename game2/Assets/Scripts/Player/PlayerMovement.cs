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
    public GameObject toRotate;

    private int _flipSide = 1;
    private bool _isMovableByPlayer = true;

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
        _rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * speed, _rb.velocity.y, 0);
        if (direction > 0)
        {
            _flipSide = 1;
            toRotate.transform.localScale = new Vector3(_flipSide, toRotate.transform.localScale.y, toRotate.transform.localScale.z);
        }
        if (direction < 0)
        {
            _flipSide = -1;
            toRotate.transform.localScale = new Vector3(_flipSide, toRotate.transform.localScale.y, toRotate.transform.localScale.z);
        }
        _player.isMoving = true;
    }
    public void MakePlayerIdle()
    {
        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        _player.isMoving = false;
    }
    public void StopPlayer()
    {
        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
    }
    public void Jump()
    {
        if (!_player.isJumping)
        {
            _player.isJumping = true;
            _player.canPlayIdleAnim = false;
            _player.canPlayWalkAnim = false;
        }
    }
    public void JumpAnimationLogic()
    {

        _rb.velocity = new Vector3(0, 0, 0);
        _rb.AddForce(new Vector2(0, jumpStrength));
        _player.isJumping = false;
    }
    private void CheckIfPlayerIsFalling()
    {
        if (_rb.velocity.y < 0) _player.isFalling = true;
        else _player.isFalling = false;
    }
}
