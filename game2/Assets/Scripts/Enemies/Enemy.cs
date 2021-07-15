using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(HealthSystem))]
public abstract class Enemy : MonoBehaviour
{
    //[SerializeField]
    //private HealthSystem hpSys;
    // Start is called before the first frame update
    [SerializeField]
    private float invicibilityProgress=0.2f;
    protected AnimationManager _anim;
    protected HealthSystem hpSys;
    public float speed;
    public int dmg;
    public int collisionDmg;

    protected bool _isAlive = true;
    protected bool _isIdle = false;
    protected bool _isHit = false;

    public event Action OnWalkEvent;
    public event Action OnAttackEvent;

    protected EnemyEnums.State currentState;
    protected Stack<EnemyEnums.State> states = new Stack<EnemyEnums.State>();
    protected virtual void SetUpComponents()
    {
        hpSys = GetComponent<HealthSystem>();
        _anim = GetComponent<AnimationManager>();
    }
    public abstract void SetPlayerInRange();
    public abstract void SetPlayerNotInRange();
    public void IncreaseInvicibilityProgress()
    {

    }
    protected virtual void StopCurrentActions()
    {
        StopAllCoroutines();
    }
    protected virtual void ResumeActions()
    {
        currentState = states.Pop();
    }

    protected virtual void Kill()
    {
        StopCurrentActions();
        //mainCollider.SetActive(false);
        _isAlive = false;
        currentState = EnemyEnums.State.DEAD;
        hpSys.isInvincible = true;
        _anim.PlayAnimation("Death");
        StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Death"), () => Destroy(gameObject)));
    }

    protected virtual void Hit()
    {
        StopCurrentActions();
        states.Push(currentState);
        _isHit = true;
        _anim.PlayAnimation("Hit");
        StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Hit"), () =>
        {
            states.Push(EnemyEnums.State.IDLE_AFTER_HIT);
            _isHit = false;
            ResumeActions();
        }));
    }

    protected IEnumerator StayIdleCor(int numberOfIdleCycles = 1)
    {
        _isIdle = true;
        if (numberOfIdleCycles > 0) _anim.PlayAnimation("Idle");
        yield return new WaitForSeconds(numberOfIdleCycles * _anim.GetAnimationLength("Idle"));
        _isIdle = false;
    }
    protected IEnumerator WaitAndExecuteFunction(float timeToWait, Action functionToPerform)
    {
        yield return new WaitForSeconds(timeToWait);
        functionToPerform();
    }
    protected void RaiseOnWalkEvent()
    {
        OnWalkEvent?.Invoke();
    }
    protected void RaiseOnAttackEvent()
    {
        OnAttackEvent?.Invoke();
    }
}
