using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelector : EnumSelector<Difficulty>
{
    protected override void OnSelectorChanged(Difficulty value)
    {
        gameplayManager.CurrentDifficulty = value;
    }
}
