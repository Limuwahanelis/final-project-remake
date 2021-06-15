using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
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
    public void Attack()
    {
        _player.isAttacking = true;
        _player.StopWalkAndIdleAnimFromPlaying();
        _player.ChangePlayerState(Player.PlayerSate.ATTACK);
        Debug.Log("attack");
        //_player.ChangePlayerState(Player.PlayerSate.ATTACK);
        _player.TakeControlFromPlayer();
        //_player.PlayAnimation("Attack1");
        //_currentState = PlayerSate.ATTACK;
    }
    public void AttackAnimFunc()
    {
        _player.isAttacking = false;
    }


}
