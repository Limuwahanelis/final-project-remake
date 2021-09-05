using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneAnimation : MonoBehaviour
{
    public string animationToPlay;
    private Animator _anim;
    public void PlayAnimation()
    {
        _anim.Play(animationToPlay);
    }

    
}
