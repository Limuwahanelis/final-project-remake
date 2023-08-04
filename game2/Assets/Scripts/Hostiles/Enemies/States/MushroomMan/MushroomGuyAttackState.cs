using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MushroomGuyAttackState : PatrollingEnemyState
{
    private AnimationManager _anim;
    private EnemyAudioManager _audio;
    private float _timer;
    public MushroomGuyAttackState(MushroomGuyContext context)
    {
       _context = context;
        _audio = context.audio;
        _anim = context.anim;
        context.OnSetPlayerInRange += DisengagePlayer;


    }
    public override void Update()
    {
        if (_timer >= _context.anim.GetAnimationLength("Attack"))
        {
            _context.ChangeState(new MushroomGuyIdleState(_context as MushroomGuyContext, this, 1));

        }
        _timer+=Time.deltaTime;
    }
    public override void SetUpState()
    {
        _anim.PlayAnimation("Attack");
        _audio.PlayAttackSound(true);
        _timer = 0;
    }
    public override void Hit()
    {
        _context.ChangeState(new MushroomGuyHitState(_context as MushroomGuyContext));
    }

    private void DisengagePlayer(bool isPlayerInRange)
    {
        (_context as MushroomGuyContext).isPlayerInRange = isPlayerInRange;
    }
    public override void InterruptState()
    {
        (_context as MushroomGuyContext).OnSetPlayerInRange -= DisengagePlayer;
    }
}
