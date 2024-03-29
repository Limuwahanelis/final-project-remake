using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public enum playerDirection
    {
        LEFT=-1,
        SAME=0,
        RIGHT=1
    }

    public playerDirection newPlayerDirection;
    public playerDirection oldPlayerDirection;
    public Ringhandle wallJumpHandle;
    [SerializeField] Player _player;
    [SerializeField] Collider2D[] _playerCols;
    private Rigidbody2D _rb;
    public float speed;
    public float jumpStrength;
    public float wallJumpStrength = Mathf.Abs( 0.5f * (7 / ((-1.154064f / 2) * 0.02f))); // _player mass * (wanted _speed/((walljumphandle vector.x/2)*fixed _time))
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

    //TO DO do smth about this
    //
    public void ChangeSpriteToWallHang()
    {
        GetComponentInChildren<SpriteRenderer>().sprite=wallHangSprite;
    }
    public void ChangeSpriteToWallJump()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = wallJumpSprite;
    }


    //


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
    public void MovePlayerForward()
    {
            _rb.velocity = new Vector3((int)oldPlayerDirection*speed, _rb.velocity.y, 0);
    }
    public void RotatePlayerOppositeDirection()
    {
        RotatePlayer((int)-_player.mainBody.transform.localScale.x);
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
       // _player.isJumping = true;
        Debug.Log(wallJumpHandle.GetPushVector() * wallJumpStrength);
        _rb.AddForce(wallJumpHandle.GetPushVector()*wallJumpStrength ,ForceMode2D.Impulse);
        //_rb.AddForce(new Vector2(0.5f,0.5f) * wallJumpStrength, ForceMode2D.Impulse);
    }
    public void Jump()
    {
        _rb.velocity = new Vector3(0, 0, 0);
        _rb.AddForce(new Vector2(0, jumpStrength));
       // _player.isJumping = false;
    }

    public bool CheckIfPlayerIsFalling()
    {
        if (_rb.velocity.y < 0) return true;
        else return false;
    }

    public void PushPlayer(Vector3 PushForce, IPlayerPusher playerPusher)
    {
        StopPlayer();
        _player.currentState.Push(playerPusher, _playerCols);
        _rb.AddForce(PushForce, ForceMode2D.Impulse);
        
        //StartCoroutine(PushCor());
        
    }
    public void PushPlayer(playerDirection pushDirection,Vector3 PushForce, IPlayerPusher playerPusher)
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
        _player.currentState.Push(playerPusher, _playerCols);
        _rb.AddForce(PushForce, ForceMode2D.Impulse);

       // StartCoroutine(PushCor());

    }
    public void SetRbYAxis(bool canMove)
    {
        if (canMove) _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        else _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
    }
    public void SetGravityScale(float value) // normal scale is 2
    {
        _rb.gravityScale = value;
    }


    public Vector2 GetPlayerVelocity()
    {
        return _rb.velocity;
    }
    public void StartAirAttack()
    {
            _rb.gravityScale = 0;
           _rb.velocity = new Vector2(airAttackSpeed * _player.mainBody.transform.localScale.x, 0);
    }
    public void StopAirAttack()
    {
        _rb.velocity = new Vector2(0, 0);
        _rb.gravityScale = 2;
    }
    //public IEnumerator AirAttackCor(float airAttackDuration)
    //{
    //    _rb.gravityScale = 0;
    //    _rb._velocity = new Vector2(airAttackSpeed * _player.mainBody.transform.localScale.x, 0);
    //    yield return new WaitForSeconds(airAttackDuration);
    //    _rb._velocity = new Vector2(0, 0);
    //    _rb.gravityScale = 2;
    //}
    //public IEnumerator JumpCor()
    //{
    //    _rb.sharedMaterial = _player.noFrictionMat;
    //    while (_player.isOnGround) yield return null;
        
    //    //_player.isJumping = false;
    //    _player.ChangeState(new PlayerInAirState(_player));
    //}

}
