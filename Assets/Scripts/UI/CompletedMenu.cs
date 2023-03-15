using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedMenu : MonoBehaviour
{

    #region Fields

    public VoidEventChannel GameCompletedEventChannel;

    #endregion

    #region Methods

    private void Start()
    {
        GameCompletedEventChannel.OnEventRaised += GameCompleted;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameCompletedEventChannel.OnEventRaised -= GameCompleted;
    }

    private void GameCompleted()
    {
        gameObject.SetActive(true);
        GameManager.Instance?.SetGameMode(GameMode.PauseMenu);
    }

    #endregion

}
