using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyAnimator/new State")]
public class MyAnimatorState : ScriptableObject
{

    public AnimationClip animation;
    public Animation an;
    public float length;

    public void PlayAnimation()
    {
    }

    private void OnValidate()
    {
        length = animation.length;
    }
}
