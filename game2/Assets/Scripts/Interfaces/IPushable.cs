using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable
{
    void Push(PlayerHealthSystem.DamageType damageType);
    void Push(PlayerMovement.playerDirection direction, PlayerHealthSystem.DamageType damageType);
}
