using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConsoleToFileLogger : MonoBehaviour
{

    #region Fields

    private StreamWriter _writer;

    #endregion

    #region Methods

    private void Start()
    {
        if (!Directory.Exists(DataPath.LogFolder))
            Directory.CreateDirectory(DataPath.LogFolder);
        var file = File.Create(DataPath.LogFile);
        _writer = new StreamWriter(file);
        Application.logMessageReceived += HandleLog;

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
        _writer.Close();
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        _writer.WriteLine($"{DateTime.Now} [{type}] {logString}\n{stackTrace}");
    }

    #endregion

}
