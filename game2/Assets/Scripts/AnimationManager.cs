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
    public List<string> stateNames = new List<string>();
    public List<float> stateLengths = new List<float>();
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
        string clipToPlayName = null;
        int index = stateNames.FindIndex((x) => x == name);
        if (index == -1)
        {
            Debug.LogError("There is no state with name: " + name);
            return;
        }
        clipToPlayName = stateNames[index];
        if (_currentAnimation == clipToPlayName) return;
        if (!canBePlayedOver)
        {
            _overPlayAnimationEnded = false;
            _animLength = stateLengths[index];
            _currentTimer = StartCoroutine(TimerCor(_animLength, SetOverPlayAnimAsEnded));
            _anim.Play(Animator.StringToHash(clipToPlayName)); //clipToPlay.nameHash);
            _currentAnimation = clipToPlayName;
        }
        if (_overPlayAnimationEnded)
        {
            _animLength = stateLengths[index];
            StartCoroutine(TimerCor(_animLength, SetNormalAnimAsEneded));
            _anim.Play(Animator.StringToHash(clipToPlayName)); //clipToPlay.nameHash);
            _currentAnimation = clipToPlayName;
        }
    }

    public void OverPlayAnimation(string name)
    {
        string clipToPlayName = null;
        clipToPlayName = stateNames.Find((x) => x == name);

        if (clipToPlayName == null)
        {
            Debug.LogError("There is no state with name: " + name);
            return;
        }
        //if (_currentAnimation == clipToPlay.name) return;
        if (_currentTimer != null) StopCoroutine(_currentTimer);
        _overPlayAnimationEnded = true;

        _anim.Play(Animator.StringToHash(clipToPlayName)); //clipToPlay.nameHash);
        _currentAnimation = clipToPlayName;
    }

    public float GetAnimationLength(string name)
    {
        float clipDuration = 0;
        int index = stateNames.FindIndex((x) => x == name);
        clipDuration = stateLengths[index];
        return clipDuration;
    }

    public float GetCurrentAnimationRemainingLength()
    {
        return _anim.GetCurrentAnimatorStateInfo(0).normalizedTime * _animLength;
    }

    IEnumerator TimerCor(float time, Action functionToPerform)
    {
        yield return new WaitForSeconds(time);
        functionToPerform();
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
