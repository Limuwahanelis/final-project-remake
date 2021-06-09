using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    private string _currentAnimation;
    private Animator _anim;
    private IAnimatable _objectToAnimate;
    public AnimatorController _animController;
    public List<string> stateNames = new List<string>();
    private void Start()
    {
        _objectToAnimate = GetComponent<IAnimatable>();
       _objectToAnimate.OnPlayAnimation += PlayAnimation;
       _anim = GetComponent<Animator>();
        
    }
    private void OnValidate()
    {
        stateNames.Clear();
        for (int i = 0; i < _animController.layers[0].stateMachine.states.Length; i++)
        {
            stateNames.Add(_animController.layers[0].stateMachine.states[i].state.name);
        }
    }
    public void PlayAnimation(string name)
    {
        
        AnimatorState clipToPlay=null;
        for (int i = 0; i < _animController.layers[0].stateMachine.states.Length; i++)
        {
            if (_animController.layers[0].stateMachine.states[i].state.name == name) clipToPlay = _animController.layers[0].stateMachine.states[i].state;
        }
        
        //Debug.Log("to play: " + clipToPlay.name);
        if (_currentAnimation == clipToPlay.name) return;
        if (clipToPlay == null)
        {
            Debug.LogError("There is no state with name: " + name);
            return ;
        }
        Debug.Log(clipToPlay.name);
        _anim.Play(clipToPlay.nameHash);
        _currentAnimation = clipToPlay.name;
        return ;
    }
    private void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1")) Debug.Log("Attacking");
    }
    private void OnDestroy()
    {
        _objectToAnimate.OnPlayAnimation -= PlayAnimation;
    }
}