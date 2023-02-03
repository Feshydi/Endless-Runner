using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    Playing,
    Paused
}


public class GameManager : MonoBehaviour
{

    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance == null ? new GameManager() : _instance;

    private GameManager() => _instance = this;

    #endregion

    #region Fields

    [Header("Data")]
    [SerializeField]
    private LevelData _levelData;

    [Header("Setting")]
    [SerializeField]
    private LevelLauncher _levelLauncher;

    [SerializeField]
    private GameMode _gameMode;

    [SerializeField]
    private Action OnGameStatusChanged;

    #endregion

    #region Properties

    public LevelData LevelData => _levelData;

    public GameMode GameMode => _gameMode;

    #endregion

    #region Methods

    private void Start()
    {
        _levelLauncher.CreateLevel(_levelData);
        _gameMode = GameMode.Playing;
    }

    private void OnApplicationPause(bool pause)
    {
        _gameMode = GameMode.Paused;
    }

    private void OnApplicationFocus(bool focus)
    {
        _gameMode = GameMode.Playing;
    }

    #endregion

}
