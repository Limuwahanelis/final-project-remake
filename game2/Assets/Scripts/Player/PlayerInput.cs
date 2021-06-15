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
        if (_player.isControlledByPlayer)
        {
            if (direction != 0)
            {
                Move(direction);
            }
            else
            {
                SetIdle();
            }
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
            }
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
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
    private void Attack()
    {
        _player.playerCombat.Attack();
        _player.playerMovement.StopPlayer();
    }
    private void Jump()
    {
        _player.playerMovement.Jump();
    }
}
