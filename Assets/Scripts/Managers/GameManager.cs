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

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is Null");

            return _instance;
        }
    }

    #endregion

    #region Fields

    [Header("Data")]
    [SerializeField]
    public LevelData LevelData;

    [SerializeField]
    public SettingsData SettingsData;

    [SerializeField]
    private GameMode _gameMode;

    #endregion

    #region Properties

    public GameMode GameMode => _gameMode;

    [System.NonSerialized]
    public Action OnGameStatusChanged;

    #endregion

    #region Methods

    private void Awake()
    {
        if (_instance != null)
            return;

        _instance = this;

        _gameMode = GameMode.Paused;

        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationPause()
    {
        _gameMode = GameMode.Paused;
        OnGameStatusChanged?.Invoke();
    }

    private void OnApplicationFocus()
    {
        OnGameStatusChanged?.Invoke();
    }

    private void AssignLevelData(LevelData levelData)
    {
        LevelData = levelData;
    }

    public void SetGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
        OnGameStatusChanged?.Invoke();
    }

    #endregion

}
