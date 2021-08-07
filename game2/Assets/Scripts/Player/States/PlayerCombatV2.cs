using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatV2 : MonoBehaviour
{
    private PlayerV2 _player;
    private AnimationManager anim;
    //private Collider2D

    public Transform attackPos;
    public LayerMask enemyLayer;
    public float attackRange;
    public int attackDamage;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerV2>();
        anim = _player.anim;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Attack(PlayerState state)
    {

        _player.playerMovement.StopPlayer();
        _player.anim.PlayAnimation("Attack1");
        StartCoroutine(AttackCor());
        StartCoroutine(_player.WaitAndExecuteFunction(_player.anim.GetAnimationLength("Attack1"), () =>
        {
            state.AttackIsOver();
        }));
    }

    public void AirAttack()
    {
        _player.canPerformAirAttack = false;
        _player.isAirAttacking = true;
        _player.anim.PlayAnimation("Air attack");
        _player.playerMovement.AirAttackAnimationLogic(anim.GetAnimationLength("Air attack"));
        StartCoroutine(AttackCor());
        StartCoroutine(_player.WaitAndExecuteFunction(anim.GetAnimationLength("Air attack"), () =>
        {
            _player.isAirAttacking = false;
        }));
    }

    IEnumerator AttackCor()
    {

        List<Collider2D> hitEnemies = new List<Collider2D>(Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer));
        int index = 0;
        for (; index < hitEnemies.Count; index++)
        {
            IDamagable tmp = hitEnemies[index].GetComponentInParent<IDamagable>();
            if (tmp != null) tmp.TakeDamage(attackDamage);
        }
        yield return null;
        while (_player.isAttacking)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!hitEnemies.Contains(colliders[i]))
                {
                    hitEnemies.Add(colliders[i]);
                    IDamagable tmp = colliders[i].GetComponentInParent<IDamagable>();
                    if (tmp != null) tmp.TakeDamage(attackDamage);
                }
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
