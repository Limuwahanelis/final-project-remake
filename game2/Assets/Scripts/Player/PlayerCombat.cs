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
        if (_player.isOnGround && !_player.isAnimationPlaying)
        {
            _player.isAttacking = true;
            _player.StopWalkAndIdleAnimFromPlaying();
            _player.TakeControlFromPlayer(Player.Cause.ATTACK);
        }
        if(!_player.isOnGround)
        {
            if(!_player.isAirAttacking)
            {
                if (_player.canPerformAirAttack)
                {
                    _player.isAirAttacking = true;
                    _player.TakeControlFromPlayer(Player.Cause.ATTACK);
                }
            }
        }
    }
    public void AttackAnimFunc()
    {
        _player.isAttacking = false;
    }

    //public void checkf()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, EnemyLayer);
    //    Debug.Log("hiotdasda");
    //    for (int i = 0; i < hitEnemies.Length; i++)
    //    {
    //        if (airAttack)
    //        {
    //            if (!hitCollsDuringAirAttack.Contains(hitEnemies[i]))
    //            {
    //                hitEnemies[i].transform.GetComponentInParent<IDamagable>().TakeDamage(attackDamage);
    //                hitCollsDuringAirAttack.Add(hitEnemies[i]);
    //            }
    //            else continue;
    //        }
    //        Debug.Log("hit");
    //        hitEnemies[i].transform.GetComponentInParent<IDamagable>().TakeDamage(attackDamage);

    //    }
    //}

}
