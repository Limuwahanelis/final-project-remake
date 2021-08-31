using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalAudioSlider : MonoBehaviour
{
    public FloatReference globalAudio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGlobalAudioValue(float newValue)
    {
        globalAudio.value = newValue;
    }
}
