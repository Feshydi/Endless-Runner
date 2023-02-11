using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreboardRow : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private TextMeshProUGUI _username;

    [SerializeField]
    private TextMeshProUGUI _score;

    [SerializeField]
    private TextMeshProUGUI _time;

    [SerializeField]
    private TextMeshProUGUI _seed;

    #endregion

    #region Methods

    public void Init(ScoreboardRowData scoreboardRowData)
    {
        _username.text = scoreboardRowData.Username;
        _score.text = scoreboardRowData.Score.ToString();
        TimeSpan timeSpan = TimeSpan.FromSeconds(scoreboardRowData.Time);
        _time.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        _seed.text = scoreboardRowData.Seed.ToString();
    }

    #endregion
}
