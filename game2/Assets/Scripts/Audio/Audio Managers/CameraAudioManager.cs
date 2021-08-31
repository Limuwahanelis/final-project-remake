using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAudioManager : MonoBehaviour
{
    public AudioEvent mainTheme;
    AudioSource source;
    public FloatReference globalAudio;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        mainTheme.Play(source);
        source.volume = mainTheme.volume*globalAudio.value;
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = mainTheme.volume * globalAudio.value;
    }

    public void ChangeVolume(float value)
    {
        Debug.Log("fire");
        source.volume = mainTheme.volume * value;
    }
    private void OnDestroy()
    {
    }
}
