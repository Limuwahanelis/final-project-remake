using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomGuyContext : PatrollingEnemyContext
{
    public MushroomGuyContext (int numberOfIdleCycles):base(numberOfIdleCycles)
    {
        
    }
    public void SetPlayerInRange(bool value) { OnSetPlayerInRange?.Invoke(value); }
    public Action<bool> OnSetPlayerInRange;
    public Action Rotate;
    public bool isPlayerInRange;
    public EnemyAudioManager audio;
}
