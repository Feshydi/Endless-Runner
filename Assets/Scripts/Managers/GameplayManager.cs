using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Gameplay Manager")]
public class GameplayManager : ScriptableObject
{

    #region Fields

    [SerializeField] private ModesList _modesList;
    [SerializeField] private DifficultiesList _difficultiesList;

    [Header("Data")]
    public PlayerControllerBehaviour Player;
    public LevelData LevelData;

    public bool AutoSeedGeneration;
    public int Seed;

    public Mode CurrentGameplayMode;
    public Difficulty CurrentDifficulty;

    #endregion

    #region Methods

    public void InitModes() =>
        _modesList.Init(CurrentGameplayMode);

    public GameplayMode<float> GetGameplayModeStats() =>
        _modesList.GetGameplayModeStats(CurrentGameplayMode);

    public GameplayDifficulty GetGameplayDifficulty() =>
        _difficultiesList.GetGameplayDifficulty(CurrentDifficulty);

    #endregion

}
