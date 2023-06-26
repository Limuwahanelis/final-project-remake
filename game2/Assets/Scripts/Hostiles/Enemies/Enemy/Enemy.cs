using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(AnimationManager))]
public abstract class Enemy : MonoBehaviour
{
    //[SerializeField]
    //private HealthSystem hpSys;
    // Start is called before the first frame update
    [SerializeField] float invicibilityProgress=0.2f;
    public BoolReference isGamePaused;
    public AnimationManager _anim;
    protected HealthSystem hpSys;
    protected EnemyAudioManager _audioMan;
    public float speed;
    public int dmg;

    protected bool _isAlive = true;
    protected bool _isIdle = false;
    protected bool _isHit = false;

    protected EnemyState state;

    protected virtual void SetUpComponents()
    {
        hpSys = GetComponent<HealthSystem>();
        _anim = GetComponent<AnimationManager>();
        _audioMan = GetComponent<EnemyAudioManager>();
    }
    public AnimationManager  GetAnimationManager()
    {
        return _anim;
    }
    public virtual void SetPlayerInRange() { }
    public virtual void SetPlayerNotInRange() { }
    public void IncreaseInvicibilityProgress()
    {

    }
    protected virtual void StopCurrentActions()
    {
        StopAllCoroutines();
    }

    //protected virtual void Kill()
    //{
    //    StopCurrentActions();
    //    //mainCollider.SetActive(false);
    //    _isAlive = false;
    //    currentState = EnemyEnums.State.DEAD;
    //    hpSys.isInvincible = true;
    //    _anim.PlayAnimation("Death");
    //    StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Death"), () => Destroy(gameObject)));
    //}

    //protected virtual void Hit()
    //{
    //    StopCurrentActions();
    //    states.Push(currentState);
    //    _isHit = true;
    //    _anim.PlayAnimation("Hit");
    //    StartCoroutine(WaitAndExecuteFunction(_anim.GetAnimationLength("Hit"), () =>
    //    {
    //        states.Push(EnemyEnums.State.IDLE_AFTER_HIT);
    //        _isHit = false;
    //        ResumeActions();
    //    }));
    //}
    public IEnumerator StayIdleCor(int numberOfIdleCycles = 1)
    {
        _isIdle = true;
        if (numberOfIdleCycles > 0) _anim.PlayAnimation("Idle");
        yield return new WaitForSeconds(numberOfIdleCycles * _anim.GetAnimationLength("Idle"));
        _isIdle = false;
    }
    public IEnumerator WaitAndExecuteFunction(float timeToWait, Action functionToPerform)
    {
        yield return new WaitForSeconds(timeToWait);
        functionToPerform();
    }
}
