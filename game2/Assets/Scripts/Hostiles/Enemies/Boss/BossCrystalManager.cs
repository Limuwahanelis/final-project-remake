﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrystalManager : MonoBehaviour
{
    public Action OnAttackEnded;
    public BossCrystalAttackPattern[] patterns;
    public BossCrystal[] crystals;
    public Boss boss;
    
    private bool startAttacks = false;
    public float patternDelay = 1f;
    private bool crystalsAreMovingBack=false;
    private bool moveThemBack = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startAttacks)
        {
            startAttacks = false;
            StartCoroutine(CrystalAttacksCor());
        }
        if(crystalsAreMovingBack)
        {
            bool crystalsAreBack = true;
            foreach (BossCrystal crystal in crystals)
            {
                if (!crystal.isBackInPos) crystalsAreBack = false;
            }
            if(crystalsAreBack)
            {
                foreach (BossCrystal crystal in crystals)
                {
                    crystal.SetRotate(true);
                }
                crystalsAreMovingBack = false;
                OnAttackEnded?.Invoke();
            }
        }
        if(moveThemBack)
        {
            MoveCrystalsBack();
            moveThemBack = false;
        }
    }
    public void StartCrystalAttacks()
    {
        startAttacks = true;
    }
    IEnumerator CrystalAttacksCor()
    {
        startAttacks = false;
        foreach (BossCrystalAttackPattern pattern in patterns)
        {
            pattern.StartPattern();
            while (!pattern.PatternHasEnded) yield return null;
            yield return new WaitForSeconds(patternDelay);
        }
        moveThemBack = true;
    }

    private void MoveCrystalsBack()
    {
        foreach (BossCrystal crystal in crystals)
        {
            crystal.MoveBack();
        }
        crystalsAreMovingBack = true;
    }
    public void DestroyCrystals()
    {
        startAttacks = false;
        foreach (BossCrystal crystal in crystals)
        {
            crystal.gameObject.SetActive(false);
        }
    }
}
