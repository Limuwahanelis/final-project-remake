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
    }
    private PlayerSate _currentState;
    public PlayerMovement playerMovement;
    public PlayerInput playerInput;
    public PlayerCombat playerCombat;
    private Animator _anim;
    private AnimatorStateInfo _currentAnimatorState;
    private bool _isAnimationPlaying = false;
    public event Action<string> OnPlayAnimation;
    public bool isControlledByPlayer = true;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectAnimationLogic();
    }
    private void SelectAnimationLogic()
    {
        switch (_currentState)
        {
            case PlayerSate.IDLE: break;
            case PlayerSate.MOVE: break;
            case PlayerSate.ATTACK:
                {
                    Debug.Log("Should play at");

                    _currentAnimatorState = _anim.GetCurrentAnimatorStateInfo(0);

                    StartCoroutine(WaitForAnimationToEnd(_currentAnimatorState.length));
                    break;
                }
            default: break;
        }
        _currentState = PlayerSate.NONE;
    }

    IEnumerator WaitForAnimationToEnd(float animationTime)
    {
        if (_isAnimationPlaying)
        {
            yield break;
        }
        else
        {
            _isAnimationPlaying = true;
        }
        yield return new WaitForSeconds(animationTime);
        isControlledByPlayer = true;
        _isAnimationPlaying = false;
    }
    public void PlayAnimation(string name)
    {
        OnPlayAnimation?.Invoke(name);
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
}
