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
        var timeSpan = TimeSpan.FromSeconds(scoreboardRowData.Time);
        if (timeSpan.Hours >= 1)
            _time.text = "OVER AN HOUR";
        else
            _time.text = timeSpan.ToString("mm':'ss'.'ff");
        _seed.text = scoreboardRowData.Seed.ToString();
    }

    #endregion
}
