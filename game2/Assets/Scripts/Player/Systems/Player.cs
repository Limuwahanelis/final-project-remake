using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [SerializeField] CorutineHolder corutineHolder;
    public PlayerMovement playerMovement;
    public AbilityList abilities;
    public PlayerInput playerInput;
    public PlayerCombat playerCombat;
    public PlayerChecks playerChecks;
    public PlayerAudioManager audioManager;
    public PlayerHealthSystem healthSystem;

    public GameObject mainBody;
    public AnimationManager anim;

    public GameObject slideColliders;
    public GameObject normalColliders;
    public PlayerState currentState;

    public GameObject darkPanel;
    public GameObject gameOverScreen;

    public PhysicsMaterial2D noFrictionMat;

    public BoolReference isGamePaused;

    public bool isJumping = false;
    public bool isOnGround = true;
    public bool isAlive = true;
    public bool isAttacking = false;
    public bool isAirAttacking = false;
    public bool canPerformAirAttack = true;
    public bool isInAirAfterPush = false;
    public bool isNearWall = false;
    public bool hasWallJumped = false;
    public bool isNearCeiling = false;
    // Start is called before the first frame update
    void Start()
    {
        healthSystem.OnDeathEvent = SetPlayerDead;
        PlayerContext playerContext = new PlayerContext()
        {
            playerMovement = playerMovement,
            SetSlideMode = SetSlideMode,
            ChangeState = ChangeState,
            WaitAndExecuteFunction = WaitAndExecuteFunction,
            anim = anim,
            audioManager = audioManager,
            playerChecks = playerChecks,
            abilityList = abilities,
            noFrictionMat = noFrictionMat,
            corutineHolder = corutineHolder,
            playerCombat = playerCombat,
            maximumNumberOfwallJumps = 1,
            numberOfPerformedWallJumps = 0,
            canPerformAirAttack = true,
            playerHealthSystem = healthSystem,

        };
        currentState = new PlayerNormalState(playerContext);
        anim = GetComponent<AnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused.value)
        {
            currentState.Update();
        }
    }
    public void SetSlideMode(bool slideMode)
    {
        if(slideMode)
        {
            slideColliders.SetActive(true);
            normalColliders.SetActive(false);
        }
        else
        {
            slideColliders.SetActive(false);
            normalColliders.SetActive(true);
        }
    }
    //public void Slide()
    //{
    //    ChangeState(new PlayerSlideState(this));
    //}
    public IEnumerator WaitAndExecuteFunction(float timeToWait, Action function)
    {
        yield return new WaitForSeconds(timeToWait);
        function();
    }
    public void ChangeState(PlayerState newState)
    {
        Debug.Log(newState.GetType());
        currentState.InterruptState();
        currentState = newState;
        currentState.SetUpState();
    }

    //public IEnumerator LeaveCeilingCor()
    //{
    //    while(isNearCeiling)
    //    {
    //        yield return null;
    //    }
    //    ChangeState(new PlayerNormalState(this));
    //    playerMovement.StopPlayer();
    //    slideColliders.SetActive(false);
    //    normalColliders.SetActive(true);
    //    StopAllCoroutines();
    //}

    public void LoadData(PlayerData playerData)
    {
        transform.position = playerData.position;
        playerCombat.attackDamage.value = playerData.damage;
        for(int i=0;i<abilities.abilities.Count;i++)
        {
            if(playerData.abilities[i]) abilities.UnlockAbility((AbilityList.Abilities)i);
            else abilities.LockAbility((AbilityList.Abilities)i);
        }
    }
    public void SetPlayerDead()
    {
        currentState.Kill();
        ShowGameOverScreen();
    }
    public void ShowGameOverScreen()
    {
        darkPanel.SetActive(true);
        gameOverScreen.SetActive(true);
    }
}
