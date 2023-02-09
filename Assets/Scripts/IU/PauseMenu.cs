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
        Time.timeScale = 0;

        GameManager.Instance.SetGameMode(GameMode.Paused);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;

        GameManager.Instance.SetGameMode(GameMode.Playing);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexes.MainMenu);
    }

    #endregion

}
