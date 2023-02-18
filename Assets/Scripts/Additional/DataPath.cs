using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataPath
{
    public static readonly string Scoreboard = $"{Application.persistentDataPath}/highscores.json";
    public static readonly string Settings = $"{Application.persistentDataPath}/settings.json";

    public static readonly string LogFolder = $"{Application.persistentDataPath}/logs";
    public static readonly string LogFile = $"{LogFolder}/{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.ff'TZ'K")}.txt";
}
