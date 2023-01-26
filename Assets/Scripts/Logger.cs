using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{

    #region Fields

    [Header("Settings")]
    [SerializeField]
    private bool _showLogs;

    [SerializeField]
    private string _prefix;

    [SerializeField]
    private Color _prefixColor;

    private string _hexColor;

    #endregion

    #region Methods

    private void Awake()
    {
        _hexColor = "#" + ColorUtility.ToHtmlStringRGBA(_prefixColor);
    }

    public void Log(object message, Object sender)
    {
        if (!_showLogs)
            return;

        Debug.Log($"<color={_hexColor}>{_prefix}</color>: {message}", sender);
    }

    #endregion

}