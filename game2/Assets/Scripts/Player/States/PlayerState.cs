using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player _player;

    public PlayerState(Player player)
    {
        _player = player;
    }
    public virtual void Attack() { }
    public virtual void AttackIsOver() { }
    public virtual void Jump() { }

    public virtual void Move(float direction) { }
    public abstract void Update();
}
