using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosBeamAttackBosState : BossState
{
    public PlayerPosBeamAttackBosState(Boss boss,BossContext context) : base(boss,context)
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
        yield return new WaitForSeconds(_context.attackDelay);
        for (int i = 0; i < 10; i++)
        {
            _context.beams[i].transform.position = new Vector3(_context.playerTrans.position.x, _context.beams[i].transform.position.y);
            _context.bossAudio.PlayBeamAudio(_context.beams[i].GetComponent<AudioSource>());
            _context.beams[i].GetComponent<DelayedBeam>().SetCor();
            if (i > 3)
            {
                _context.beams[i - 4].GetComponent<DelayedBeam>().DisableCor();
                _context.beams[i - 4].transform.localPosition = _context.delayedBeamPos;
            }
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 6; i < 10; i++)
        {
            _context.beams[i].GetComponent<DelayedBeam>().DisableCor();
            _context.beams[i].transform.localPosition = _context.delayedBeamPos;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(_context.attackDelay);
        Debug.Log("end attack");
        _context.attackPatten++;
        _boss.ChangeState(new BossMoveToVulnerablePosState(_boss,new BossNonTargetedBeamAttackState(_boss, _context), _context));
    }
}
