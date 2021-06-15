using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAnimator : ScriptableObject
{
    public List<MyAnimatorState> states = new List<MyAnimatorState>();
    MyAnimatorState FindState(string state)
    {
       return states.Find((x => x.name == state));
    }
}
