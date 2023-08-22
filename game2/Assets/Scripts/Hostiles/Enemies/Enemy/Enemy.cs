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
    [SerializeField] protected float _speed;
    [SerializeField] protected int _dmg;
    [SerializeField] protected AnimationManager _anim;
    [SerializeField] protected BoolReference _isGamePaused;
    protected EnemyAudioManager _audioMan;
    protected HealthSystem hpSys;

    protected bool _isAlive = true;

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
    public void ChangeState(EnemyState newState)
    {
        //Debug.Log(newState);
        state.InterruptState();
        state = newState;
        state.SetUpState();
    }
    public virtual void SetPlayerInRange() { }
    public virtual void SetPlayerNotInRange() { }
    public void IncreaseInvicibilityProgress()
    {

    }
    public IEnumerator WaitAndExecuteFunction(float timeToWait, Action functionToPerform)
    {
        yield return new WaitForSeconds(timeToWait);
        functionToPerform();
    }
}
