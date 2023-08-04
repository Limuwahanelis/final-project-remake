using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyContext
{
    public Action<EnemyState> ChangeState; 
}
