using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayModeSelector : EnumSelector<Mode>
{
    public GameplayModeManager _gameplayModeManager;

    private void OnEnable()
    {
        onValueChanged += OnModeChanged;
    }

    private void OnDisable()
    {
        onValueChanged -= OnModeChanged;
    }

    private void OnModeChanged(Mode mode)
    {
        _gameplayModeManager.CurrentGameplayMode = mode;
    }
}
