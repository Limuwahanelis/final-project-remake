using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour,IAnimatable
{
    public enum PlayerSate
    {
        NONE,
        IDLE,
        MOVE,
        ATTACK,
        JUMP,
        FALLING,
        AIRATTACK,

    }
    private PlayerSate _currentState;
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;
    public PlayerCombat playerCombat;
    public GameObject mainBody;
    private Animator _anim;
    public bool isAnimationPlaying = false;
    public bool isJumping = false;

    public event Action<string> OnPlayAnimation;
    public event Func<string,float> OnGetAnimationLength;

    public bool isControlledByPlayer = true;
    public bool isOnGround = true;
    public bool isMoving = false;
    public bool isAttacking = false;
    public bool canPlayIdleAnim = true;
    public bool canPlayWalkAnim = true;
    public bool isFalling = false;
    

    public bool isAirAttacking = false;
    public bool canPerformAirAttack = true;

    public bool test = false;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        //StartCoroutine(WaitForAnimationToEnd(10f, (result => test = result),test));
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        SelectAnimationToPlay();
        SelectAnimationLogic();
        //SelectAnimationLogic();
        //if (!_isPlayingOtherAnimation) PlayAnimation("Idle");
    }
    private void SelectAnimationToPlay()
    {
        if (!isAnimationPlaying)
        {
            if (isOnGround)
            {
                if (isControlledByPlayer)
                {
                    if (isMoving)
                    {
                        if (canPlayWalkAnim) PlayAnimation("Walk");
                    }
                    else if (canPlayIdleAnim)
                    {
                        PlayAnimation("Idle");
                    }
                }
                if (isJumping) PlayAnimation("Jump");
                if (isAttacking) PlayAnimation("Attack1");
            }
            else
            {
                if (isFalling) PlayAnimation("Fall");
                if (isAirAttacking) PlayAnimation("Air attack");
            }
        }
    }
    private void SelectAnimationLogic()
    {
        switch (_currentState)
        {
            case PlayerSate.IDLE: break;
            case PlayerSate.MOVE: break;
            case PlayerSate.JUMP:
                {
                    canPlayIdleAnim = false;
                    canPlayWalkAnim = false;
                    StartCoroutine(WaitForAnimationToEnd(GetAnimationLength("Jump"), (result => isJumping = result), isJumping));
                    break;
                }
            case PlayerSate.ATTACK:
                {
                    Debug.Log("Should play at");

                    

                    StartCoroutine(WaitForAnimationToEnd(GetAnimationLength("Attack1"), (result => isAttacking = result), isAttacking));
                    break;
                }
            case PlayerSate.AIRATTACK:
                {
                    canPerformAirAttack = false;
                    StartCoroutine(WaitForAnimationToEnd(GetAnimationLength("Air attack"), (result => isAirAttacking = result), isAirAttacking));
                    playerMovement.AirAttackAnimationLogic(GetAnimationLength("Air attack"));
                    break;
                }
            default:
                {
                    //PlayAnimation("Idle");
                    break;
                }

        }
        _currentState = PlayerSate.NONE;
    }


    public void PlayAnimation(string name)
    {
        OnPlayAnimation?.Invoke(name);
    }
    public float GetAnimationLength(string name)
    {
        if (OnGetAnimationLength != null)
        {
            Debug.Log(OnGetAnimationLength.Invoke(name));
            
        }
        return OnGetAnimationLength.Invoke(name);
    }
    public void ChangePlayerState(PlayerSate newState)
    {
        _currentState = newState;
    }

    public void ReturnControlToPlayer()
    {
        isControlledByPlayer = true;
        Debug.Log("End");
    }
    public void TakeControlFromPlayer()
    {
        isControlledByPlayer = false;
        Debug.Log("take");
    }
    public void StopWalkAndIdleAnimFromPlaying()
    {
        canPlayWalkAnim = false;
        canPlayIdleAnim = false;
    }

    IEnumerator WaitForAnimationToEnd(float animationTime,Action<bool> myVar,bool currentValue)
    {
        if (isAnimationPlaying)
        {
            yield break;
        }
        else
        {
            isAnimationPlaying = true;
        }
       
        yield return new WaitForSeconds(animationTime);
        myVar(!currentValue);
        canPlayWalkAnim = true;
        canPlayIdleAnim = true;
        ReturnControlToPlayer();
        isAnimationPlaying = false;
    }
    private void Function1(ref bool val)
    {
        val = !val;
    }


}
