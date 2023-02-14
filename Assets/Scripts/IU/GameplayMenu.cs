using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayMenu : MonoBehaviour
{

    #region Methods

    public void ContinueGame()
    {
        GameManager.Instance.SetGameMode(GameMode.Playing);
    }

    public void RestartGame()
    {
        SaveGameStatistic();
        GameManager.Instance.LoadingManager.RestartLevel();
    }

    public void LoadMainMenu()
    {
        SaveGameStatistic();
        GameManager.Instance.LoadingManager.LoadMenu();
    }

    private void SaveGameStatistic()
    {
        var scoreManager = GameManager.Instance.ScoreManager;
        scoreManager.AddScore(scoreManager.GetCurrentScore());
    }

    #endregion

}
