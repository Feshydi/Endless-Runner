using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataPath
{
    public static readonly string Scoreboard = $"{Application.persistentDataPath}/highscores.json";
    public static readonly string Settings = $"{Application.persistentDataPath}/settings.json";
}
