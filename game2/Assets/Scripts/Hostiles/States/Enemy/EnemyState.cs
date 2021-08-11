using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState 
{
    //protected Enemy enemy;
    protected bool canChangeState;
    public abstract void Update();
    public virtual void SetUpState( ) { }
    public virtual void ChangeState() { }

    public bool CheckIfStateCanBeChanged()
    {
        return canChangeState;
    }
}
