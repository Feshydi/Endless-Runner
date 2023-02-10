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

    [Header("Data")]
    [SerializeField]
    public LevelData LevelData;

    [SerializeField]
    public SettingsData SettingsData;

    [SerializeField]
    public SceneLoadingManager LoadingManager;

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
        _instance = this;
        SetGameMode(GameMode.MainMenu);

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

    private void AssignLevelData(LevelData levelData)
    {
        LevelData = levelData;
    }

    public void SetGameMode(GameMode gameMode)
    {
        _gameMode = gameMode;
        OnGameStatusChanged?.Invoke();

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
