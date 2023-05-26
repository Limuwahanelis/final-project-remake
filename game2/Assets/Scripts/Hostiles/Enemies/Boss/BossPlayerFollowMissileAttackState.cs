using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerFollowMissileAttackState : BossState
{
    public BossPlayerFollowMissileAttackState(Boss boss,BossContext context) : base(boss,context)
    {
    }

    public override void Update()
    {
        
    }
    public override void SetUpState()
    {
        _boss.StartCoroutine(AttackCor3());
    }
    IEnumerator AttackCor3()
    {
        yield return new WaitForSeconds(_context.attackDelay);
        for (int i = 0; i < 20; i++)
        {
            GameObject missile =Object.Instantiate(_context.missilePrefab, _context.missileSpawnPos, _context.missilePrefab.transform.rotation);
            missile.transform.up = _context.playerTrans.position - _context.missileSpawnPos;
            AudioSource tmpSource = missile.AddComponent<AudioSource>();
            _boss.GetComponent<BossAudioManager>().PlayAttackSound(tmpSource);
            missile.GetComponent<Missile>().SetSpeed(10);
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(_context.attackDelay);
        _context.attackPatten = 1;
         _boss.ChangeState(new BossMoveToVulnerablePosState(_boss,new BossNonTargetedBeamAttackState(_boss,_context),_context));
    }
}
