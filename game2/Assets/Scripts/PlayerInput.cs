using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        //_player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        if (direction != 0)
        {

            //_player.
            //_currentState = PlayerSate.MOVE;
            Move(direction);
        }
        else
        {
            SetIdle();
            //_rb.velocity = new Vector3(0, _rb.velocity.y, 0);
            //_currentState = PlayerSate.IDLE;
            //PlayAnimation("Idle");
        }
        if (Input.GetButtonDown("Attack"))
        {
            //Debug.Log("attack");
            //_isMovableByPlayer = false;
            //PlayAnimation("Attack1");
            //_currentState = PlayerSate.ATTACK;


        }
    }

    private void Move(float direction)
    {
        _player.playerMovement.MovePlayer(direction);
    }

    private void SetIdle()
    {
        _player.playerMovement.MakePlayerIdle();
    }
}
