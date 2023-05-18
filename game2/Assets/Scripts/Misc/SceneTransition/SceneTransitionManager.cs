using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneTransitionManager
{
    public enum TransitionTags
    {
        A,B,C,D,NONE
    }

    public static TransitionTags tagToTeleportPlayer=TransitionTags.NONE;
}
