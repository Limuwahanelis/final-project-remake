using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState 
{
    //protected Enemy enemy;
    public abstract void Update();
    public virtual void SetUpState( ) { }
    public virtual void ChangeState() { }
    public virtual void Hit() { }

    public virtual void InterruptState() { }
}
