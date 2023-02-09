using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Slider _soundVolume;

    #endregion

    #region Methods

    private void OnEnable()
    {
        var settings = GameManager.Instance.SettingsData;
        _soundVolume.value = settings.SoundVolume;
    }

    private void OnDisable()
    {
        var settings = GameManager.Instance.SettingsData;
        settings.SoundVolume = _soundVolume.value;
    }

    #endregion

}
