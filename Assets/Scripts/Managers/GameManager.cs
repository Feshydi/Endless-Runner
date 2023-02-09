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
    public GameMode GameMode;

    [System.NonSerialized]
    public Action OnGameStatusChanged;

    #endregion

    #region Methods

    private void Awake()
    {
        _instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        GameMode = GameMode.Playing;
        OnGameStatusChanged?.Invoke();
    }

    private void OnApplicationPause(bool pause)
    {
        GameMode = GameMode.Paused;
        OnGameStatusChanged?.Invoke();
    }

    private void OnApplicationFocus(bool focus)
    {
        GameMode = GameMode.Playing;
        OnGameStatusChanged?.Invoke();
    }

    private void OnApplicationQuit()
    {
        
    }

    private void AssignLevelData(LevelData levelData)
    {
        LevelData = levelData;
    }

    #endregion

}
