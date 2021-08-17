using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCollider : MonoBehaviour
{
    public bool isOnGround=false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOnGround = false;
    }
}
