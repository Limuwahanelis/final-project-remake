using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "bool list")]
public class BoolList : ScriptableObject
{
    public List<bool> values;
    public void AddNewValue(bool value)
    {
        values.Add(value);
    }

    public void ChangeValue(int index,bool newValue)
    {
        values[index] = newValue;
    }

    public bool GetValue(int index)
    {
        return values[index];
    }
}
