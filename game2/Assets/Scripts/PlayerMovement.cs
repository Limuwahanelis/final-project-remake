using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerMovement : MonoBehaviour
{
    //enum PlayerSate
    //{
    //    NONE,
    //    IDLE,
    //    MOVE,
    //    ATTACK,
    //}
    //private PlayerSate _currentState;
    [SerializeField]
    private Player _player;
    private Rigidbody2D _rb;
    //private Animator _anim;
    public float speed;
    public GameObject toRotate;
    //public event Action<string> OnPlayAnimation;
    private int _flipSide = 1;
    private bool _isMovableByPlayer = true;
    //private AnimatorStateInfo _currentAnimatorState;
    //private bool _isAnimationPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //SelectAnimationLogic();
        //float direction = Input.GetAxisRaw("Horizontal");
        //if (_isMovableByPlayer)
        //{
            
        //    if (direction != 0)
        //    {
        //        _currentState = PlayerSate.MOVE;
        //        MovePlayer(direction);
        //    }
        //    else
        //    {
        //        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        //        _currentState = PlayerSate.IDLE;
        //        PlayAnimation("Idle");
        //    }
        //    if (Input.GetButtonDown("Attack"))
        //    {
        //        Debug.Log("attack");
        //        _isMovableByPlayer = false;
        //        PlayAnimation("Attack1");
        //        _currentState = PlayerSate.ATTACK;
                
                
        //    }
        //}
        //else
        //{
        //    _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        //}
        //if (_currentAnimatorState.IsName("Attack1")) Debug.Log("dsadas");
        //else
        //{
        //    Debug.Log("aaaaaa");
        //}
        //
    }

    // Is called at the beginning of the frame because 
    //animator switches state at the beginning of the frame
    //private void SelectAnimationLogic()
    //{
    //    switch (_currentState)
    //    {
    //        case PlayerSate.IDLE:break;
    //        case PlayerSate.MOVE:break;
    //        case PlayerSate.ATTACK:
    //            {
    //                Debug.Log("Should play at");
                    
    //                _currentAnimatorState = _anim.GetCurrentAnimatorStateInfo(0);

    //                StartCoroutine(WaitForAnimationToEnd(_currentAnimatorState.length));
    //                break;
    //            }
    //        default: break;
    //    }
    //    _currentState = PlayerSate.NONE;
    //}
    //public void PlayAnimation(string name)
    //{
    //    OnPlayAnimation?.Invoke(name);
    //}
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
        _player.ChangePlayerState(Player.PlayerSate.MOVE);
        _player.PlayAnimation("Walk");
    }
    public void MakePlayerIdle()
    {
        _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
        _player.ChangePlayerState(Player.PlayerSate.IDLE);
        _player.PlayAnimation("Idle");
    }

    //IEnumerator WaitForAnimationToEnd(float animationTime)
    //{
    //    if (_isAnimationPlaying)
    //    {
    //        yield break;
    //    }
    //    else
    //    {
    //        _isAnimationPlaying = true;
    //    }
    //    yield return new WaitForSeconds(animationTime);
    //    _isMovableByPlayer = true;
    //    _isAnimationPlaying = false;
    //}
}
