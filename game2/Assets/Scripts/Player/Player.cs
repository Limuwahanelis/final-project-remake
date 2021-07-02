using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    public enum Cause
    {
        NONE,
        ENEMY,
        COLLISION,
        OVERRIDE,
        ATTACK,
        JUMP,
        DEATH
    }
    public Cause NoControlCause = Cause.NONE;
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;
    public PlayerCombat playerCombat;
    public GameObject mainBody;
    public AnimationManager anim;

    public bool isJumping = false;
    public bool isMovableByPlayer = true;
    public bool isOnGround = true;
    public bool isMoving = false;
    public bool isAttacking = false;
    public bool isFalling = false;
    public bool isAlive = true;

    public bool isAirAttacking = false;
    public bool canPerformAirAttack = true;

    public bool test = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<AnimationManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            if (isMovableByPlayer)
            {
                if(isOnGround)
                {
                    canPerformAirAttack = true;
                    if (isMoving) anim.PlayAnimation("Walk");
                    else anim.PlayAnimation("Idle");
                }
            }

            if(!isOnGround)
            {
                if (isFalling) anim.PlayAnimation("Fall");
            }
        }
    }
    public void ReturnControlToPlayer(Cause returnControlCause)
    {
        if (NoControlCause == Cause.NONE) return;
        if (returnControlCause == Cause.OVERRIDE)
        {
            isMovableByPlayer = true;
            NoControlCause = Cause.NONE;
            return;
        }
        if (NoControlCause != returnControlCause) return;
        else
        {
            isMovableByPlayer = true;
        }
        NoControlCause = Cause.NONE;
    }

    public void TakeControlFromPlayer(Cause takeAwayCause)
    {
        isMovableByPlayer = false;
        NoControlCause = takeAwayCause;
        playerMovement.StopPlayer();
    }
    public IEnumerator WaitAndExecuteFunction(float timeToWait,Action function)
    {
        yield return new WaitForSeconds(timeToWait);
        function();
    }


}
