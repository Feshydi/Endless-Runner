using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Settings
{

    #region Fields

    public string Username;

    [Range(0, 1)]
    public float MasterVolume;

    [Range(0, 1)]
    public float MusicVolume;

    [Range(0, 1)]
    public float EffectsVolume;

    #endregion

    #region Methods

    public void SetDefaultValues()
    {
        Username = "player";
        MasterVolume = 0.5f;
        MusicVolume = 0.5f;
        EffectsVolume = 0.5f;
    }

    public bool IsNull()
    {
        if (Username == "")
            return true;
        else
            return false;
    }

    #endregion

}
