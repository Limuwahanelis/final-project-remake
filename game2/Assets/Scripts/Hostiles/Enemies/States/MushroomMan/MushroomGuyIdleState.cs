using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGuyIdleState : EnemyState
{
    private AnimationManager _anim;
    private MushroomGuyEnemy _enemy;
    private EnemyAudioManager _audio;
    public MushroomGuyIdleState(MushroomGuyEnemy enemy)
    {
        _enemy = enemy;
        _anim = _enemy.GetAnimationManager();
        _audio = _enemy.GetAudioManager();
    }

    public override void Update()
    {
    }
}
