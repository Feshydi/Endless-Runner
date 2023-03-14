using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelector : EnumSelector<Difficulty>
{
    public GameplayModeManager _gameplayModeManager;

    private void OnEnable()
    {
        onValueChanged += OnDifficultyChanged;
    }

    private void OnDisable()
    {
        onValueChanged -= OnDifficultyChanged;
    }

    private void OnDifficultyChanged(Difficulty difficulty)
    {
        _gameplayModeManager.CurrentDifficulty = difficulty;
    }
}
