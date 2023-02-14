using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Texture2D _crosshairTexture;

    #endregion

    #region Methods

    private void Start()
    {
        GameManager.Instance.OnGameStatusChanged += UpdateCursor;

        DontDestroyOnLoad(gameObject);
    }

    private void UpdateCursor()
    {
        switch (GameManager.Instance.GameMode)
        {
            case GameMode.Playing:
                SetCrosshair();
                break;
            default:
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
