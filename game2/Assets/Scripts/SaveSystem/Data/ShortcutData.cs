using Gamekit2D;
using System;

using UnityEngine;

[Serializable]
public class ShortcutData
{
    public string Scene1;
    public string Scene2;
    public string Id;
    public bool IsUnlocked;

    public ShortcutData(ShortcutState shortcutState)
    {
        Id = shortcutState.Id;
        Scene1 = shortcutState.Scene1;
        Scene2 = shortcutState.Scene2;
        IsUnlocked = shortcutState.IsUnlocked;
    }
}
