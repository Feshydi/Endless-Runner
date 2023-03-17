using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private GameplayManager _gameplayManager;

    [SerializeField]
    private int _currentScorePoints;

    [SerializeField]
    private int _maxScoreboardRows;

    [SerializeField]
    private string _savePath;

    public List<ScoreboardRowData> Highscores;

    public FloatEventChannel OnScorePointsEvent;

    #endregion

    #region Methods

    private void Start()
    {
        if (!Directory.Exists(DataPath.ScoreboardFolder))
            Directory.CreateDirectory(DataPath.ScoreboardFolder);
    }

    public void Init()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ResetScore()
    {
        _currentScorePoints = 0;
        OnScorePointsEvent.RaiseEvent(_currentScorePoints);
    }
    public void AddScore(int value)
    {
        _currentScorePoints += value;
        OnScorePointsEvent.RaiseEvent(_currentScorePoints);
    }

    public void UpdateHighscores()
    {
        Highscores = GetSavedScores();
    }

    public void AddScore(ScoreboardRowData scoreboardRowData)
    {
        var highscores = GetSavedScores();

        var _isAdded = false;
        for (int i = 0; i < highscores.Count; i++)
        {
            if (scoreboardRowData.Score > highscores[i].Score)
            {
                highscores.Insert(i, scoreboardRowData);
                _isAdded = true;
                break;
            }
        }
        if (!_isAdded)
            highscores.Add(scoreboardRowData);

        if (highscores.Count > _maxScoreboardRows)
            highscores.RemoveRange(_maxScoreboardRows, highscores.Count - _maxScoreboardRows);

        SaveScores(highscores);
    }

    private List<ScoreboardRowData> GetSavedScores()
    {
        _savePath = GetSavePath();
        if (!File.Exists(_savePath))
            File.Create(_savePath).Dispose();

        using (StreamReader stream = new StreamReader(_savePath))
        {
            string json = stream.ReadToEnd();

            var highscores = JsonConvert.DeserializeObject<List<ScoreboardRowData>>(json);
            if (highscores != null)
                return highscores;
        }

        return new List<ScoreboardRowData>();
    }

    private void SaveScores(List<ScoreboardRowData> scoreboardRowDatas)
    {
        _savePath = GetSavePath();
        using (StreamWriter stream = new StreamWriter(_savePath))
        {
            string json = JsonConvert.SerializeObject(scoreboardRowDatas);
            stream.Write(json);
        }
    }

    public ScoreboardRowData GetCurrentScore()
    {
        var gameManager = GameManager.Instance;
        return new ScoreboardRowData(gameManager.SettingsManager.Settings.Username,
            _currentScorePoints,
            gameManager.CurrentLevelManager.TimerController.Elapsedtime,
            _gameplayManager.Seed);
    }

    private string GetSavePath()
    {
        return DataPath.ScoreboardFolder + "/"
           + _gameplayManager.CurrentGameplayMode + "_"
            + _gameplayManager.CurrentDifficulty + ".json";
    }

    #endregion

}
