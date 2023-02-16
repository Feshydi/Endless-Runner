using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private TMP_InputField _username;

    [SerializeField]
    private Slider _soundVolume;

    #endregion

    #region Methods

    private void OnEnable()
    {
        UpdateUI(GameManager.Instance.SettingsManager.Settings);
    }

    private void OnDisable()
    {
        Settings settings;
        settings.Username = _username.text;
        settings.SoundVolume = _soundVolume.value;
        GameManager.Instance.SettingsManager.UpdateSettingsData(settings);
    }

    private void UpdateUI(Settings settings)
    {
        _username.text = settings.Username;
        _soundVolume.value = settings.SoundVolume;
    }

    #endregion

}
