using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio Event")]
public class AudioEvent : ScriptableObject
{
    [Range(0,1)]
    public float volume;
    [Range(0, 1)]
    public float pitch;

    public virtual void Play(AudioSource audioSource) { }

}
