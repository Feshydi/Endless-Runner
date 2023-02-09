using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Settings")]
public class SettingsData : ScriptableObject
{

    #region Fields

    [SerializeField]
    [Range(0, 1)]
    public float SoundVolume;

    #endregion

}
