using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObject : ScriptableObject
{
    public Animator anim;
    public AnimationClip clip;
    private AnimationClip _currentClip;
    [HideInInspector]
    public int clipHash;
    private void OnValidate()
    {
        if(_currentClip!=clip)
        {
            _currentClip = clip;
            clipHash=Animator.StringToHash(clip.name);
        }
    }
}
