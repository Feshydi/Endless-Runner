using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode
{
    MainMenu,
    PauseMenu,
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

    [Header("Managers")]
    public SceneLoadingManager SceneLoadingManager;

    public ScoreManager ScoreManager;

    public CursorManager CursorManager;

    [Header("Data")]
    public LevelData LevelData;

    public SettingsData SettingsData;

    public SceneLoadingManager LoadingManager;

    public LevelManager CurrentLevelManager;

    [SerializeField]
    private GameMode _gameMode;

    public event Action<GameMode> OnGameStatusChanged;

    #endregion

    #region Properties

    public GameMode GameMode => _gameMode;

    #endregion

    #region Methods

    private void Awake()
    {
        _instance = this;
        SceneLoadingManager.Init();
        ScoreManager.Init();
        CursorManager.Init(this);

        DontDestroyOnLoad(gameObject);
    }

    private void OnApplicationPause()
    {
        if (_gameMode.Equals(GameMode.Playing))
            SetGameMode(GameMode.Paused);
    }

    private void OnApplicationFocus()
    {
        if (_gameMode.Equals(GameMode.Paused))
            SetGameMode(GameMode.Playing);
    }

    public void SetGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
        OnGameStatusChanged?.Invoke(_gameMode);

        switch (_gameMode)
        {
            case GameMode.MainMenu:
                Time.timeScale = 0;
                break;
            case GameMode.PauseMenu:
                Time.timeScale = 0;
                break;
            case GameMode.Playing:
                Time.timeScale = 1;
                break;
            case GameMode.Paused:
                Time.timeScale = 0;
                break;
        }
    }

    #endregion

}
