using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    #region Methods

    private void OnEnable()
    {
        PauseGame();
    }

    private void OnDisable()
    {
        ContinueGame();
    }

    private void PauseGame()
    {
        GameManager.Instance.SetGameMode(GameMode.PauseMenu);
    }

    public void ContinueGame()
    {
        GameManager.Instance.SetGameMode(GameMode.Playing);
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
