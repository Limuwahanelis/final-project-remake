using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerContext _playerContext;

    public PlayerState(PlayerContext playerContext)
    {
        _playerContext = playerContext;
    }
    public virtual void DropBomb() { }
    public virtual void Attack() { }
    public virtual void AttackIsOver() { }
    public virtual void Jump() { }
    public virtual void Move(float direction) { }

    public virtual void OnHit() { }
    public virtual void SetUpState() { }

    public virtual void Slide() { }
    public virtual void InterruptState() { }
    public abstract void Update();
}
