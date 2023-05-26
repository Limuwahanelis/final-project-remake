using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlayerFollowMissileAttackState : BossState
{
    public BossPlayerFollowMissileAttackState(Boss boss) : base(boss)
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
        yield return new WaitForSeconds(_boss.attackDelay);
        for (int i = 0; i < 20; i++)
        {
            GameObject missile =Object.Instantiate(_boss.missilePrefab, _boss.missileSpawn.transform.position, _boss.missilePrefab.transform.rotation);
            missile.transform.up = _boss.player.transform.position - _boss.missileSpawn.transform.position;
            AudioSource tmpSource = missile.AddComponent<AudioSource>();
            _boss.GetComponent<BossAudioManager>().PlayAttackSound(tmpSource);
            missile.GetComponent<Missile>().SetSpeed(10);
            yield return new WaitForSeconds(0.4f);
        }
        yield return new WaitForSeconds(_boss.attackDelay);
        _boss.attackPatten = 1;
         _boss.ChangeState(new BossMoveToVulnerablePosState(_boss,new BossNonTargetedBeamAttackState(_boss)));
    }
}
