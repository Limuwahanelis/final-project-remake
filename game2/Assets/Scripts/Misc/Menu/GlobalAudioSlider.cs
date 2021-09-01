using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GlobalAudioSlider : MonoBehaviour
{
    public FloatReference globalAudio;
    private Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = globalAudio.value;
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
