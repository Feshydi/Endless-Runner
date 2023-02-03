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
    private LevelManager _levelManager;

    [SerializeField]
    private GameMode _gameMode;

    [System.NonSerialized]
    public Action OnGameStatusChanged;

    #endregion

    #region Properties

    public LevelData LevelData => _levelData;

    public GameMode GameMode => _gameMode;

    #endregion

    #region Methods

    private void Start()
    {
        _levelManager.CreateLevel(_levelData);
        _gameMode = GameMode.Playing;
        OnGameStatusChanged?.Invoke();
    }

    private void OnApplicationPause(bool pause)
    {
        _gameMode = GameMode.Paused;
        OnGameStatusChanged?.Invoke();
    }

    private void OnApplicationFocus(bool focus)
    {
        _gameMode = GameMode.Playing;
        OnGameStatusChanged?.Invoke();
    }

    #endregion

}
