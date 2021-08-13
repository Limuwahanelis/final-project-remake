using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    int dmg = 40;
    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("spikesall");
        IDamagable player = collision.transform.GetComponent<PlayerHealthSystem>();
        player.TakeDamage(dmg);
        Debug.Log("spikes");
    }
}
