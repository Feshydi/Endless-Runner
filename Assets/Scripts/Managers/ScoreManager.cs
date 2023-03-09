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
    private LoadedLevelData _loadedLevelData;

    [SerializeField]
    private int _currentScorePoints;

    [SerializeField]
    private int _maxScoreboardRows;

    [SerializeField]
    private string _savePath;

    public List<ScoreboardRowData> Highscores;

    public event Action<int> OnScorePointsEvent;

    #endregion

    #region Methods

    public void Init()
    {
        _savePath = DataPath.Scoreboard;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetScore()
    {
        _currentScorePoints = 0;
        OnScorePointsEvent?.Invoke(_currentScorePoints);
    }
    public void AddScore(int value)
    {
        _currentScorePoints += value;
        OnScorePointsEvent?.Invoke(_currentScorePoints);
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
            _loadedLevelData.Seed);
    }

    #endregion

}
