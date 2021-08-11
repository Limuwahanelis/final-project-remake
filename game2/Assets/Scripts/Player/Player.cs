using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;
    public PlayerCombat playerCombat;
    public GameObject mainBody;
    public AnimationManager anim;

    public PlayerState currentState;

    public bool isJumping = false;
    public bool isOnGround = true;
    public bool isAlive = true;
    public bool isAttacking = false;
    public bool isAirAttacking = false;
    public bool canPerformAirAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        currentState = new PlayerNormalState(this);
        anim = GetComponent<AnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update();
    }

    public IEnumerator WaitAndExecuteFunction(float timeToWait, Action function)
    {
        yield return new WaitForSeconds(timeToWait);
        function();
    }
    public void ChangeState(PlayerState newState)
    {
        currentState = newState;
    }
}
