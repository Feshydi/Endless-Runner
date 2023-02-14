using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public struct ScoreboardRowData
{
    [SerializeField]
    public string Username;

    public int Score;

    public float Time;

    [SerializeField]
    public int Seed;

    public ScoreboardRowData(string username, int score, float time, int seed)
    {
        Username = username;
        Score = score;
        Time = time;
        Seed = seed;
    }
}

public class Scoreboard : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Transform _contextHolderTransform;

    [SerializeField]
    private GameObject _scoreboardTitlePrefab;

    [SerializeField]
    private ScoreboardRow _scoreboardRowPrefab;

    [SerializeField]
    private List<ScoreboardRowData> _highscores;

    #endregion

    #region Methods

    private void OnEnable()
    {
        var scoreManager = GameManager.Instance.ScoreManager;
        scoreManager.UpdateHighscores();
        _highscores = scoreManager.Highscores;
        UpdateUI(_highscores);
    }

    private void UpdateUI(List<ScoreboardRowData> scoreboardRowDatas)
    {
        foreach (Transform child in _contextHolderTransform)
        {
            Destroy(child.gameObject);
        }

        Instantiate(_scoreboardTitlePrefab, _contextHolderTransform);

        foreach (var scoreboardRowData in scoreboardRowDatas)
        {
            Instantiate(_scoreboardRowPrefab, _contextHolderTransform)
                .Init(scoreboardRowData);
        }
    }

    #endregion

}
