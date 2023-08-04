using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    AudioEvent walkingAudioEvent;
    [SerializeField]
    AudioEvent combatAudioEvent;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //combat.OnAttackEvent += PlayAttackSound;
    }

    // Update is called once per frame
    void Update()
    {

    }

   public void PlayWalkingSound()
    {
        walkingAudioEvent.Play(audioSource);
    }
    public virtual void PlayAttackSound()
    {
        combatAudioEvent.Play(audioSource);
    }
    public virtual void PlayAttackSound(bool overPlay=false)
    {
        combatAudioEvent.Play(audioSource,overPlay);
    }
}
