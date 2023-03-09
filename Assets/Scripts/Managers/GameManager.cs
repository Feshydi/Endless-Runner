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
    // Paused exists for situations when game was accidentally collapsed
    // or hidden by another window
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
    public SceneLoadingManager LoadingManager;

    public ScoreManager ScoreManager;

    public CursorManager CursorManager;

    public SettingsManager SettingsManager;

    public LevelManager CurrentLevelManager;

    [Header("Data")]
    [SerializeField]
    private InputReader _inputReader;

    [SerializeField]
    private GameMode _gameMode;

    public event Action<GameMode> OnGameStatusChanged;

    #endregion

    #region Properties

    public GameMode GameMode => _gameMode;

    #endregion

    #region Methods

    private void Start()
    {
        _instance = this;
        LoadingManager.Init();
        ScoreManager.Init();
        CursorManager.Init(this);
        SettingsManager.Init();

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
                _inputReader.DisableAllInput();
                Time.timeScale = 0;
                break;
            case GameMode.PauseMenu:
                _inputReader.DisableAllInput();
                Time.timeScale = 0;
                break;
            case GameMode.Playing:
                _inputReader.EnableGameplayInput();
                Time.timeScale = 1;
                break;
            case GameMode.Paused:
                _inputReader.DisableAllInput();
                Time.timeScale = 0;
                break;
        }
    }

    #endregion

}
