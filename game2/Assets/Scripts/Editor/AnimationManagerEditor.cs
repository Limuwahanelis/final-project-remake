using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
[CustomEditor(typeof(AnimationManager))]
public class AnimationManagerEditor : Editor
{
    private void OnEnable()
    {
        AnimationManager man = (AnimationManager)target;
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
                man.stateNames.Add("Empty");
                man.stateLengths.Add(0);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
