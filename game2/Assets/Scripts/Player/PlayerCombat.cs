using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Player _player;
    private AnimationManager anim;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        anim = _player.anim;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack()
    {
        if (_player.isOnGround && !_player.isAttacking && _player.isMovableByPlayer)
        {
            _player.playerMovement.StopPlayer();
            _player.isAttacking = true;
            _player.TakeControlFromPlayer(Player.Cause.ATTACK);
            _player.anim.PlayAnimation("Attack1");
            StartCoroutine(_player.WaitAndExecuteFunction(_player.anim.GetAnimationLength("Attack1"), () =>
             {
                 _player.isAttacking = false;
                 _player.ReturnControlToPlayer(Player.Cause.ATTACK);
             }));
        }
        if (!_player.isOnGround && !_player.isAttacking && _player.isMovableByPlayer)
        {
            if (_player.canPerformAirAttack)
            {
                _player.canPerformAirAttack = false;
                _player.isAirAttacking = true;
                _player.isAttacking = true;
                _player.anim.PlayAnimation("Air attack");
                _player.TakeControlFromPlayer(Player.Cause.ATTACK);
                _player.playerMovement.AirAttackAnimationLogic(anim.GetAnimationLength("Air attack"));
                StartCoroutine(_player.WaitAndExecuteFunction(anim.GetAnimationLength("Air attack"), () =>
                 {
                     _player.isAirAttacking = false;
                     _player.isAttacking = false;
                     _player.ReturnControlToPlayer(Player.Cause.ATTACK);
                 }));
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
