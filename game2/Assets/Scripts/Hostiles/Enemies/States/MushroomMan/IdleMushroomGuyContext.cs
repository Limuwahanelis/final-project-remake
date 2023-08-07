using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMushroomGuyContext:EnemyContext
{
    public void SetPlayerInRange(bool value) { OnSetPlayerInRange?.Invoke(value); }
    public void SetPlayerBehind() { OnSetPlayerBehind?.Invoke(); }
    public AnimationManager anim;
    public Action<bool> OnSetPlayerInRange;
    public Action OnSetPlayerBehind;
    public Action Rotate;
    public bool isPlayerInRange;
    public EnemyAudioManager audio;
    public bool isPlayerBehind;
}
