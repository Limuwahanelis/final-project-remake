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
    [SerializeField] protected float _speed;
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
    public IEnumerator WaitAndExecuteFunction(float timeToWait, Action functionToPerform)
    {
        yield return new WaitForSeconds(timeToWait);
        functionToPerform();
    }
}
