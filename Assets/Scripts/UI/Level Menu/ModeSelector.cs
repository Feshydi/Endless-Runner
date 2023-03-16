using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelector : EnumSelector<Mode>
{
    protected override void OnSelectorChanged(Mode value)
    {
        gameplayManager.CurrentGameplayMode = value;
    }
}
