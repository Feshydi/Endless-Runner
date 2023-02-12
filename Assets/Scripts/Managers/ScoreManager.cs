using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        if (Highscores == null)
            Highscores = new List<ScoreboardRowData>();
    }

    public void AddScore(ScoreboardRowData scoreboardRowData)
    {
        var highscores = GetSavedScores();

        for (int i = 0; i < highscores.Count; i++)
        {
            if (scoreboardRowData.Score > highscores[i].Score)
            {
                highscores.Insert(i, scoreboardRowData);
                SaveScores(highscores);
                UpdateHighscores();
                return;
            }
        }
    }

    private List<ScoreboardRowData> GetSavedScores()
    {
        if (!File.Exists(SavePath))
        {
            File.Create(SavePath).Dispose();
            return new List<ScoreboardRowData>();
        }

        using (StreamReader stream = new StreamReader(SavePath))
        {
            string json = stream.ReadToEnd();

            return JsonUtility.FromJson<List<ScoreboardRowData>>(json);
        }
    }

    private void SaveScores(List<ScoreboardRowData> scoreboardRowDatas)
    {
        using (StreamWriter stream = new StreamWriter(SavePath))
        {
            string json = JsonUtility.ToJson(scoreboardRowDatas, true);
            stream.Write(json);
        }
    }

    #endregion

}
