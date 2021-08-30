using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

[CreateAssetMenu(menuName ="Animation duration list")]
public class AnimationDurationList : ScriptableObject
{
    [SerializeField]
    public List<Ann> animations;
#if UNITY_EDITOR
    [HideInInspector]
    public AnimatorController animatorController;
    public void RefreshList()
    {
        animations.Clear();
        for (int i = 0; i < animatorController.layers[0].stateMachine.states.Length; i++)
        {
            AnimatorState state = animatorController.layers[0].stateMachine.states[i].state;
            if (state.motion == null)
            {
                animations.Add(new Ann(state.name, 0)); 
                continue;
            }
            animations.Add(new Ann(state.name, state.motion.averageDuration));
        }
    }
#endif
}
