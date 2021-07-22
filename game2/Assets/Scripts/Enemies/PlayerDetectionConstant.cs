using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionConstant : PlayerDetection
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        playerPos = collision.transform.position;
    }
}
