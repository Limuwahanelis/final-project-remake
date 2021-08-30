using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class AnimationDurationListEditor : Editor
{
    private AnimationDurationList list;
    private void OnEnable()
    {
        list = target as AnimationDurationList;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("refresh"))
        {
            //list.RefreshList();
        }
    }
}
