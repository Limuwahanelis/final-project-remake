using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerV2 _player;

    public PlayerState(PlayerV2 player)
    {
        _player = player;
    }
    public virtual void Attack() { }
    public virtual void AttackIsOver() { }
    public virtual void Jump() { }

    public virtual void Move(float direction) { }
    public abstract void Update();
}
