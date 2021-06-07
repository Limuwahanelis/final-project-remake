using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public interface IAnimatable
{
    public event Action<string> OnPlayAnimation;
    //public delegate void AnimationDelegate(string name);
    
    void PlayAnimation(string name);
}
