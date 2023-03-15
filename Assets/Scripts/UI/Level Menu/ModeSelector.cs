using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSelector : EnumSelector<Mode>
{
    public GameplayManager _gameplayManager;

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
        _gameplayManager.CurrentGameplayMode = mode;
    }
}
