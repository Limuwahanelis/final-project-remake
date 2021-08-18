using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
    private string _currentAnimation;
    public Animator _anim;
    [SerializeField]
    private float _animLength;
    private bool _overPlayAnimationEnded = true;
    private Coroutine _currentTimer;
#if UNITY_EDITOR
    public AnimatorController animatorController;
#else
    private void Start()
    {
        _anim = GetComponent<Animator>();
    }
#endif

    public void PlayAnimation(string name, bool canBePlayedOver = true)
    {
        if (_currentAnimation == name) return;
        if (!canBePlayedOver)
        {
            _overPlayAnimationEnded = false;
            _animLength = GetAnimationLength(name);
            _currentTimer = StartCoroutine(TimerCor(_animLength, SetOverPlayAnimAsEnded));
            _anim.Play(Animator.StringToHash(name)); //clipToPlay.nameHash);
            _currentAnimation = name;
        }
        if (_overPlayAnimationEnded)
        {
            _animLength = GetAnimationLength(name);
            StartCoroutine(TimerCor(_animLength, SetNormalAnimAsEneded));
            _anim.Play(Animator.StringToHash(name)); //clipToPlay.nameHash);
            _currentAnimation = name;
        }
    }

    public void OverPlayAnimation(string name) 
    {
        string clipToPlayName = name;

        if (_currentTimer != null) StopCoroutine(_currentTimer);
        _overPlayAnimationEnded = true;

        _anim.Play(Animator.StringToHash(clipToPlayName)); //clipToPlay.nameHash);
        _currentAnimation = clipToPlayName;
    }

    public float GetAnimationLength(string name)
    {
        if (name == "Empty") return 0;
        float clipDuration = 0;
        for (int i = 0; i < animatorController.layers[0].stateMachine.states.Length; i++)
        {
            AnimatorState state = animatorController.layers[0].stateMachine.states[i].state;
            if (state.name == name)
            {
                clipDuration = state.motion.averageDuration;
            }
        }

        return clipDuration;
    }

    public float GetCurrentAnimationRemainingLength()
    {
        return (1- _anim.GetCurrentAnimatorStateInfo(0).normalizedTime) * _animLength;
    }

    IEnumerator TimerCor(float time, Action functionToPerform)
    {
        yield return new WaitForSeconds(time);
        functionToPerform();
    }

    public void SetAnimator(bool active)
    {
        _anim.enabled = active;
    }
    private void SetOverPlayAnimAsEnded()
    {
        _overPlayAnimationEnded = true;
    }
    private void SetNormalAnimAsEneded()
    {
        _currentAnimation = null;
    }
}
