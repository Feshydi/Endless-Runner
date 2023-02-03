using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Texture2D _crosshairTexture;

    [SerializeField]
    private GameMode _currentStatus;

    #endregion

    #region Methods

    private void Update()
    {
        _currentStatus = GameManager.Instance.GameMode;

        if (_currentStatus.Equals(GameMode.Playing))
            SetCrosshair();
        else
            SetDefault();
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
