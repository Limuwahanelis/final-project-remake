using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    public LayerMask ground;
    public float groundCheckWidth;
    public float groundCheckHeight;
    public Transform groundCheckPos;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player.isOnGround)
        {
            //_player.canPlayIdleAnim = false;
            if (_player.isOnGround = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, ground))
            {
                _player.canPlayIdleAnim = true;
                _player.canPlayWalkAnim = true;
                if (_player.canPerformAirAttack == false) _player.canPerformAirAttack = true;
            }
        }
        else
        {
            _player.isOnGround = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, ground);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheckPos.position, new Vector3(groundCheckWidth, groundCheckHeight));
    }
}
