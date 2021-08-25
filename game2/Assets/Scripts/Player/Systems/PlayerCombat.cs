using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Player _player;
    //private Collider2D

    public Transform attackPos;
    public LayerMask enemyLayer;
    public float attackRange;
    public IntReference attackDamage;
    public Sprite playerHitSprite;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
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
        _player.playerMovement.AirAttackAnimationLogic(_player.anim.GetAnimationLength("Air attack"));
        StartCoroutine(AirAttackCor());
        StartCoroutine(_player.WaitAndExecuteFunction(_player.anim.GetAnimationLength("Air attack"), () =>
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
            if (tmp != null) tmp.TakeDamage(attackDamage.value);
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
                    if (tmp != null) tmp.TakeDamage(attackDamage.value);
                }
            }
            yield return null;
        }
    }
    IEnumerator AirAttackCor()
    {

        List<Collider2D> hitEnemies = new List<Collider2D>(Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer));
        int index = 0;
        for (; index < hitEnemies.Count; index++)
        {
            IDamagable tmp = hitEnemies[index].GetComponentInParent<IDamagable>();
            if (tmp != null) tmp.TakeDamage(attackDamage.value);
        }
        yield return null;
        while (_player.isAirAttacking)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!hitEnemies.Contains(colliders[i]))
                {
                    hitEnemies.Add(colliders[i]);
                    IDamagable tmp = colliders[i].GetComponentInParent<IDamagable>();
                    if (tmp != null) tmp.TakeDamage(attackDamage.value);
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