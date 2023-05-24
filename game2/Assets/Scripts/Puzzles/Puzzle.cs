using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] protected bool solved;
#endif
    public Action<Puzzle> OnSolved;
    [Space]
    [SerializeField] BoolReference isSolved;
    public abstract void MarkAsSolved();
}
