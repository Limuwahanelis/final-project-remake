﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{

    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    } 

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponentInParent<IPushable>().Push(PlayerHealthSystem.DamageType.MISSILE);
        collision.GetComponentInParent<IDamagable>().TakeDamage(damage,PlayerHealthSystem.DamageType.MISSILE);
        
    }
}
