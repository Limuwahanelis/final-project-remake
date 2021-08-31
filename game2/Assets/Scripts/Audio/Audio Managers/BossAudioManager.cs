using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAudioManager : MonoBehaviour
{
    [SerializeField]
    AudioEvent combatAudioEvent;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayAttackSound(AudioSource audioSource)
    {
        combatAudioEvent.Play(audioSource);
    }
}
