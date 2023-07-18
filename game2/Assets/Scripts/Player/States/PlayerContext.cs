using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContext
{
    public PlayerMovement playerMovement;
    public Action<PlayerState> ChangeState;
    public AnimationManager anim;
    public PlayerAudioManager audioManager;
    public PlayerChecks playerChecks;
    public AbilityList abilityList;
}
