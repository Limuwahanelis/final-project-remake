using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicPuzzle1 : Puzzle
{
    public InteractableTorch[] torches = new InteractableTorch[5];
    public GameObject hpPickUp;
    private void Start()
    {
#if UNITY_EDITOR
        if (solved)
        {
            MarkAsSolved();
            hpPickUp.SetActive(true);
        }
#endif
    }
    public void CheckIfTorchesAreLit(int torchIndex)
    {
        for(int i=0;i<5;i++)
        {
            if (!torches[i].fireActive) return;
        }
        hpPickUp.SetActive(true);
        OnSolved?.Invoke(this);
    }
    public override void MarkAsSolved()
    {
        for (int i=0;i<5;i++)
        {
            torches[i].LightUp();
            torches[i].SetInteraction(false);
            torches[i].enabled = false;
            torches[i].transform.GetComponent<Collider2D>().enabled = false;
        }
    }
}
