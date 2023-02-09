using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    #region Singleton

    private static CursorManager _instance;

    public static CursorManager Instance
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
    private Texture2D _crosshairTexture;

    [SerializeField]
    private GameMode _currentStatus;

    #endregion

    #region Methods

    private void Start()
    {
        if (_instance != null)
            return;

        _instance = this;

        GameManager.Instance.OnGameStatusChanged += UpdateCursor;

        DontDestroyOnLoad(this);
    }

    private void UpdateCursor()
    {
        _currentStatus = GameManager.Instance.GameMode;

        switch (_currentStatus)
        {
            case GameMode.Playing:
                SetCrosshair();
                break;
            case GameMode.Paused:
                SetDefault();
                break;
        }
    }

    public void SetCrosshair()
    {
        Cursor.SetCursor(_crosshairTexture, new Vector2(_crosshairTexture.width / 2, _crosshairTexture.height / 2), CursorMode.Auto);
    }

    public void SetDefault()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    #endregion

}
