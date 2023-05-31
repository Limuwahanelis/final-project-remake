using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] LayerMask ground;
    [SerializeField] float groundCheckWidth;
    [SerializeField] float groundCheckHeight;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] Transform slideColWallCheck;
    [SerializeField] Transform wallCheckPos;
    [SerializeField] Transform ceilingCheckPos;
    [SerializeField] Transform wallCheck2Pos;
    [SerializeField] float ceilingCheckWidth;
    [SerializeField] float ceilingCheckHeight;
    [SerializeField] float slideColWallCheckWidth;
    [SerializeField] float slideColWallkHeight;
    [SerializeField] float WallCheckWidth;
    [SerializeField] float WallCheckHeight;
    private Player _player;

    private Collider2D potentialWallCol;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        _player.isOnGround = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, ground);
        potentialWallCol = Physics2D.OverlapBox(wallCheckPos.position, new Vector2(WallCheckWidth, WallCheckHeight), 0, ground);
        _player.isNearCeiling = Physics2D.OverlapBox(ceilingCheckPos.position, new Vector2(ceilingCheckWidth, ceilingCheckHeight), 0, ground);
        if (potentialWallCol)
        {
            if (Physics2D.OverlapBox(wallCheck2Pos.position, new Vector2(WallCheckWidth, WallCheckHeight), 0, ground))
            {
                _player.isNearWall = !potentialWallCol.CompareTag("Platform");
                _player.isNearWall = !potentialWallCol.CompareTag("Spikes");
            }
        }
        else _player.isNearWall = false;
        
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
        _player.slideColliders.SetActive(false);
        _player.normalColliders.SetActive(true);
        _player.StopAllCoroutines();
        _player.ChangeState(new PlayerNormalState(_player));
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(wallCheckPos.position, new Vector3(WallCheckWidth, WallCheckHeight));
        Gizmos.DrawWireCube(wallCheck2Pos.position, new Vector3(WallCheckWidth, WallCheckHeight));
        Gizmos.DrawWireCube(ceilingCheckPos.position, new Vector3(ceilingCheckWidth, ceilingCheckHeight));
        Gizmos.DrawWireCube(groundCheckPos.position, new Vector3(groundCheckWidth, groundCheckHeight));
        Gizmos.DrawWireCube(slideColWallCheck.position, new Vector3(slideColWallCheckWidth, slideColWallkHeight));
    }
    
}
