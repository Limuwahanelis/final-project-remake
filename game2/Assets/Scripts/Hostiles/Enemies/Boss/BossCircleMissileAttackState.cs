using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCircleMissileAttackState : BossState
{
    public BossCircleMissileAttackState(Boss boss,BossContext context) : base(boss,context)
    {
    }
    public override void Update()
    {

    }
    public override void SetUpState()
    {
        _boss.StartCoroutine(AttackCor2());
    }
    IEnumerator AttackCor2()
    {
        yield return new WaitForSeconds(_context.attackDelay);
        for (int i = 0; i < 60; i++)
        {
            GameObject missile = Object.Instantiate(_context.missilePrefab, _context.missileSpawnPos, Quaternion.Euler(0, 0, 90 + i * 20));
            AudioSource tmpSource = missile.AddComponent<AudioSource>();
            _boss.GetComponent<BossAudioManager>().PlayAttackSound(tmpSource);
            missile.GetComponent<Missile>().SetSpeed(10);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(_context.attackDelay);
        _context.attackPatten++;
        _boss.ChangeState(new BossMoveToVulnerablePosState(_boss,new BossNonTargetedBeamAttackState(_boss,_context),_context));
    }


}
