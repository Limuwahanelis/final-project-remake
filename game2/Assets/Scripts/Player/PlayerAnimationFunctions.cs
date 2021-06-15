using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationFunctions : MonoBehaviour
{
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpFunction()
    {
        _player.isJumping = false;
    }
}
