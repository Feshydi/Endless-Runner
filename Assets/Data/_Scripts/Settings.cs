using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Settings
{

    [SerializeField]
    public string Username;

    [SerializeField]
    [Range(0, 1)]
    public float SoundVolume;

    public void SetDefaultValues()
    {
        Username = "player";
        SoundVolume = 0.5f;
    }

    public bool IsNull()
    {
        if (Username == "")
            return true;
        else
            return false;
    }

}
