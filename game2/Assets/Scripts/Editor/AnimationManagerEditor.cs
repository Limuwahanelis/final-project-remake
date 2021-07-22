using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
[CustomEditor(typeof(AnimationManager))]
public class AnimationManagerEditor : Editor
{
    AnimationManager man;
    private void OnEnable()
    {
        man = (AnimationManager)target;
        
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        man._anim = man.GetComponent<Animator>();
        man.animatorController = (AnimatorController)man.GetComponent<Animator>().runtimeAnimatorController;
        man.stateNames.Clear();
        man.stateLengths.Clear();
        for (int i = 0; i < man.animatorController.layers[0].stateMachine.states.Length; i++)
        {
            AnimatorState state = man.animatorController.layers[0].stateMachine.states[i].state;

            if (state.motion != null)
            {
                man.stateNames.Add(state.name);
                man.stateLengths.Add(state.motion.averageDuration);
            }
            else
            {
                man.stateNames.Add(state.name);
                man.stateLengths.Add(0);
            }
        }
    }

}

