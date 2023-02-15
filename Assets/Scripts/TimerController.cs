using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private TextMeshProUGUI _timerCounter;

    [SerializeField]
    private float _elapsedTime;

    [SerializeField]
    private bool _timerGoing;

    #endregion

    #region Properties

    public float Elapsedtime => _elapsedTime;

    #endregion

    #region Methods

    public void Init()
    {
        _timerCounter.text = "00:00.00";
        _elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            if (GameManager.Instance.GameMode.Equals(GameMode.Playing))
            {
                _elapsedTime += Time.deltaTime;
                var timeSpan = TimeSpan.FromSeconds(_elapsedTime);
                _timerCounter.text = timeSpan.ToString("mm':'ss'.'ff");
            }
            yield return null;
        }
    }

    #endregion

}
