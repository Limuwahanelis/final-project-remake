using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private AnimationManager _man;
    // Start is called before the first frame update
    void Start()
    {
        _man = GetComponent<AnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            _man.TestPlay("Test");
        }
    }
}
