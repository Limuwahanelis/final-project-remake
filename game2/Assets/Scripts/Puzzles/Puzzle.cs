using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{
    public Action<Puzzle> OnSolved;
    public abstract void MarkAsSolved();
}
