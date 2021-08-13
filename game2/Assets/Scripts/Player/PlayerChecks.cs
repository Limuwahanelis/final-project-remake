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
    public float slideColWallCheckWidth;
    public float slideColWallkHeight;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        _player.isOnGround = Physics2D.OverlapBox(groundCheckPos.position, new Vector2(groundCheckWidth, groundCheckHeight), 0, ground);

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
        Gizmos.DrawWireCube(groundCheckPos.position, new Vector3(groundCheckWidth, groundCheckHeight));
        Gizmos.DrawWireCube(slideColWallCheck.position, new Vector3(slideColWallCheckWidth, slideColWallkHeight));
    }
    
}
