using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public FloatReference globalAudio;
    public SingleClipAudioEvent walkingSound;
    public MultipleClipsAudioEvent normalAttacksSounds;
    public SingleClipAudioEvent airAttackSound;

    private AudioSource _source;
    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNormalAttackSound()
    {
        normalAttacksSounds.Play(_source);
        _source.volume = normalAttacksSounds.volume*globalAudio.value;
    }

    public void PlayWalkSound()
    {
        walkingSound.Play(_source);
        _source.volume = walkingSound.volume * globalAudio.value;
    }

    public void PlayAirAttackSound()
    {
        airAttackSound.Play(_source);
        _source.volume = airAttackSound.volume * globalAudio.value;
    }
}
