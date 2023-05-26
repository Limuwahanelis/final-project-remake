using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosBeamAttackBosState : BossState
{
    public PlayerPosBeamAttackBosState(Boss boss) : base(boss)
    {
    }

    public override void Update()
    {

    }
    public override void SetUpState()
    {
        _boss.StartCoroutine(AttackCor1());
    }
    IEnumerator AttackCor1()
    {
        yield return new WaitForSeconds(_boss.attackDelay);
        for (int i = 0; i < 10; i++)
        {
            _boss.beams[i].transform.position = new Vector3(_boss.player.transform.position.x, _boss.beams[i].transform.position.y);
            _boss.audio.PlayBeamAudio(_boss.beams[i].GetComponent<AudioSource>());
            _boss.beams[i].GetComponent<DelayedBeam>().SetCor();
            if (i > 3)
            {
                _boss.beams[i - 4].GetComponent<DelayedBeam>().DisableCor();
                _boss.beams[i - 4].transform.localPosition = _boss.delayedBeamPos;
            }
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 6; i < 10; i++)
        {
            _boss.beams[i].GetComponent<DelayedBeam>().DisableCor();
            _boss.beams[i].transform.localPosition = _boss.delayedBeamPos;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(_boss.attackDelay);
        Debug.Log("end attack");
        _boss.attackPatten++;
        _boss.ChangeState(new BossMoveToVulnerablePosState(_boss,new BossNonTargetedBeamAttackState(_boss)));
    }
}
