using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    int dmg = 4;
    private void OnCollisionStay2D(Collision2D collision)
    {
        IDamagable player = collision.transform.GetComponent<PlayerHealthSystem>();
        IPushable toPush = collision.transform.GetComponent<PlayerHealthSystem>();
        toPush.Push();
        player.TakeDamage(dmg);
    }
}
