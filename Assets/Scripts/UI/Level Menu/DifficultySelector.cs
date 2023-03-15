using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelector : EnumSelector<Difficulty>
{
    public GameplayManager _gameplayManager;

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
        _gameplayManager.CurrentDifficulty = difficulty;
    }
}
