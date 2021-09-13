using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public enum playerDirection
    {
        LEFT=-1,
        RIGHT=1
    }

    public playerDirection newPlayerDirection;
    public playerDirection oldPlayerDirection;
    public Ringhandle wallJumpHandle;
    [SerializeField]
    private Player _player;
    private Rigidbody2D _rb;
    public float speed;
    public float jumpStrength;
    public float wallJumpStrength = Mathf.Abs( 0.5f * (7 / ((-1.154064f / 2) * 0.02f))); // player mass * (wanted speed/((walljumphandle vector.x/2)*fixed time))
    public float airAttackSpeed;
    public float slideTime = 2f;
    public GameObject toRotate;
    public Sprite wallHangSprite;
    public Sprite wallJumpSprite;
    private int _flipSide = 1;
    //private bool _isMovableByPlayer = true;

    private float _previousDirection;

    void Start()
    {
        newPlayerDirection = playerDirection.RIGHT;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void MovePlayer(float direction)
    {
        if (direction != 0)
        {
            oldPlayerDirection = newPlayerDirection;
            newPlayerDirection = (playerDirection)direction;
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
            if (_previousDirection != 0)
            {
                StopPlayerOnXAxis();
            }
        }
        _previousDirection = direction;
    }
    public void RotatePlayer(int sideToFlipTo)
    {
        _flipSide = sideToFlipTo;
        _player.mainBody.transform.localScale = new Vector3(_flipSide, _player.mainBody.transform.localScale.y, _player.mainBody.transform.localScale.z);
    }
    public void ChangeRb2DMat(PhysicsMaterial2D material)
    {
        _rb.sharedMaterial = material;
    }
    public void StopPlayer()
    {
        _rb.velocity = new Vector2(0, 0);
    }
    public void StopPlayerOnXAxis()
    {
        _rb.velocity = new Vector2(0, _rb.velocity.y);
    }

    public void WallJump()
    {
        _player.isJumping = true;
        Debug.Log(wallJumpHandle.GetPushVector() * wallJumpStrength);
        _rb.AddForce(wallJumpHandle.GetPushVector()*wallJumpStrength ,ForceMode2D.Impulse);
        //_rb.AddForce(new Vector2(0.5f,0.5f) * wallJumpStrength, ForceMode2D.Impulse);
    }
    public void Jump()
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

    public void PushPlayer(Vector3 PushForce)
    {
        StopPlayer();
        _player.ChangeState(new PlayerPushedState(_player));
        _rb.AddForce(PushForce, ForceMode2D.Impulse);
        
        StartCoroutine(PushCor());
        
    }
    public void PushPlayer(playerDirection pushDirection,Vector3 PushForce)
    {
        StopPlayer();
        if(pushDirection==playerDirection.RIGHT)
        {
            PushForce = new Vector3(Mathf.Abs(PushForce.x), PushForce.y, PushForce.z);
        }
        else
        {
            PushForce = new Vector3(-Mathf.Abs(PushForce.x), PushForce.y, PushForce.z);
        }
        _player.ChangeState(new PlayerPushedState(_player));
        _rb.AddForce(PushForce, ForceMode2D.Impulse);

        StartCoroutine(PushCor());

    }
    public void SetGravityScale(float value) // normal scale is 2
    {
        _rb.gravityScale = value;
    }    


    public Vector2 GetPlayerVelocity()
    {
        return _rb.velocity;
    }
    public IEnumerator AirAttackCor(float airAttackDuration)
    {
        _rb.gravityScale = 0;
        _rb.velocity = new Vector2(airAttackSpeed * _player.mainBody.transform.localScale.x, 0);
        yield return new WaitForSeconds(airAttackDuration);
        _rb.velocity = new Vector2(0, 0);
        _rb.gravityScale = 2;
    }
    public IEnumerator JumpCor()
    {
        _rb.sharedMaterial = _player.noFrictionMat;
        while (_player.isOnGround) yield return null;
        
        _player.isJumping = false;
        _player.ChangeState(new PlayerInAirState(_player));
    }
    public IEnumerator PushCor()
    {
        while (_player.isOnGround) yield return null;
        _player.isInAirAfterPush = true;
        _rb.sharedMaterial = _player.noFrictionMat;
        //_player.ChangeState(new PlayerPushedState(_player));
    }
}
