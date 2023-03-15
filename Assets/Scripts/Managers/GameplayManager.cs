using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
    ChildOfTheBelarus
}

[CreateAssetMenu(menuName = "Game/Gameplay Manager")]
public class GameplayManager : ScriptableObject
{

    #region Fields

    [SerializeField] private ModesList _modesList;
    [SerializeField] private DifficultiesList _difficultiesList;

    public Mode CurrentGameplayMode;
    public Difficulty CurrentDifficulty;

    #endregion

    #region Methods

    public void InitModes()
    {
        foreach (var mode in _modesList.Modes)
        {
            mode.IsEnabled = mode.Mode.Equals(CurrentGameplayMode);
            mode.Init();
        }
    }

    public GameplayMode<float> GetGameplayModeStats()
    {
        foreach (var mode in _modesList.Modes)
        {
            if (mode.Mode.Equals(CurrentGameplayMode))
                return mode;
        }
        return null;
    }

    public GameplayDifficulty GetGameplayDifficulty()
    {
        foreach (var difficulty in _difficultiesList.Difficulties)
        {
            if (difficulty.Difficulty.Equals(CurrentDifficulty))
                return difficulty;
        }
        return null;
    }
    #endregion

}
