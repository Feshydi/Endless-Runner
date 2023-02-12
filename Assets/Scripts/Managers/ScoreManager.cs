using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    #region Singleton

    private static ScoreManager _instance;

    public static ScoreManager Instance
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

    [SerializeField]
    private int _currentScorePoints;

    [SerializeField]
    public List<ScoreboardRowData> Highscores;

    [NonSerialized]
    public static Action<int> OnScorePointsChanged;

    private string SavePath => $"{Application.persistentDataPath}/highscores.json";

    #endregion

    #region Methods

    private void Awake()
    {
        _instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        _currentScorePoints = 0;
        OnScorePointsChanged?.Invoke(_currentScorePoints);
    }

    public void AddScore(int value)
    {
        _currentScorePoints += value;
        OnScorePointsChanged?.Invoke(_currentScorePoints);
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

        SaveScores(highscores);
    }

    private List<ScoreboardRowData> GetSavedScores()
    {
        if (!File.Exists(SavePath))
            File.Create(SavePath).Dispose();

        using (StreamReader stream = new StreamReader(SavePath))
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
        using (StreamWriter stream = new StreamWriter(SavePath))
        {
            string json = JsonConvert.SerializeObject(scoreboardRowDatas);
            stream.Write(json);
        }
    }

    public ScoreboardRowData GetCurrentScore()
    {
        return new ScoreboardRowData(GameManager.Instance.SettingsData.Username,
            _currentScorePoints, 1430f, GameManager.Instance.LevelData.Seed);
    }

    #endregion

}
