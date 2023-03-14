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

[CreateAssetMenu(menuName = "Game/Game Mode Manager")]
public class GameplayModeManager : ScriptableObject
{

    #region Fields

    [SerializeField] private List<GameplayMode<float>> _modes;

    public Mode CurrentGameplayMode;
    public Difficulty CurrentDifficulty;

    #endregion

    #region Methods

    public void InitModes()
    {
        foreach (var mode in _modes)
            mode.Init();
    }

    public GameplayMode<float> GetGameplayModeStats()
    {
        foreach (var mode in _modes)
        {
            if (mode.Mode.Equals(CurrentGameplayMode))
                return mode;
        }
        return null;
    }

    #endregion

}
