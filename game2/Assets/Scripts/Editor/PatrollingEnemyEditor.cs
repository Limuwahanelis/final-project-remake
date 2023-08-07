using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(PatrollingEnemy),true)]
public class PatrollingEnemyEditor : Editor
{
    PatrollingEnemy patrollingEnemy;
    SerializedObject so;

    private void OnEnable()
    {
        patrollingEnemy = (PatrollingEnemy)target;
        so = new SerializedObject(patrollingEnemy);
    }

    public override void OnInspectorGUI()
    {
        string[] exclude = new string[] { "_patrolPoints" };
        serializedObject.Update();
        DrawPropertiesExcluding(serializedObject, exclude);
        SerializedProperty patrolPos = serializedObject.FindProperty("_patrolPoints");
        EditorGUILayout.PropertyField(patrolPos, true);
        serializedObject.ApplyModifiedProperties();
        //base.OnInspectorGUI();
    }
}
