using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    public LayerMask ground;
    public float groundCheckWidth;
    public float groundCheckHeight;
    public Transform groundCheckPos;
    public Transform slideColWallCheck;
    public Transform wallCheckPos;
    public Transform ceilingCheckPos;
    public float ceilingCheckWidth;
    public float ceilingCheckHeight;
    public float slideColWallCheckWidth;
    public float slideColWallkHeight;
    public float WallCheckWidth;
    public float WallCheckHeight;
    private Player _player;

    private Collider2D platform;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        _player.isOnGround = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, ground);
        _player.isNearWall= Physics2D.OverlapBox(wallCheckPos.position, new Vector2(WallCheckWidth, WallCheckHeight), 0, ground);
        _player.isNearCeiling = Physics2D.OverlapBox(ceilingCheckPos.position, new Vector2(ceilingCheckWidth, ceilingCheckHeight), 0, ground);
        if(_player.isNearWall)
        {
            _player.isNearWall = !Physics2D.OverlapBox(wallCheckPos.position, new Vector2(WallCheckWidth, WallCheckHeight), 0, ground).CompareTag("Platform");
        }
        
    }

    public bool CheckForSlideWall()
    {
        return Physics2D.OverlapBox(slideColWallCheck.position, new Vector3(slideColWallCheckWidth, slideColWallkHeight), 0, ground);
    }

    public IEnumerator CheckForWallDuringSlideCor()
    {
        while(!CheckForSlideWall())
        {
            yield return null;
        }
        _player.StopAllCoroutines();
        _player.ChangeState(new PlayerNormalState(_player));
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(wallCheckPos.position, new Vector3(WallCheckWidth, WallCheckHeight));
        Gizmos.DrawWireCube(ceilingCheckPos.position, new Vector3(ceilingCheckWidth, ceilingCheckHeight));
        Gizmos.DrawWireCube(groundCheckPos.position, new Vector3(groundCheckWidth, groundCheckHeight));
        Gizmos.DrawWireCube(slideColWallCheck.position, new Vector3(slideColWallCheckWidth, slideColWallkHeight));
    }
    
}
