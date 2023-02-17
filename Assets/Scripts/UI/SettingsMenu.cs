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

    [SerializeField]
    private Slider _musicVolume;

    [SerializeField]
    private Slider _effectsVolume;

    #endregion

    #region Methods

    private void OnEnable()
    {
        UpdateUI(GameManager.Instance.SettingsManager.Settings);
    }

    private void OnDisable()
    {
        ApplySettings();
    }

    private void UpdateUI(Settings settings)
    {
        _username.text = settings.Username;
        _soundVolume.value = settings.MasterVolume;
        _musicVolume.value = settings.MusicVolume;
        _effectsVolume.value = settings.EffectsVolume;
    }

    private void ApplySettings()
    {
        Settings settings;
        settings.Username = _username.text;
        settings.MasterVolume = _soundVolume.value;
        settings.MusicVolume = _musicVolume.value;
        settings.EffectsVolume = _effectsVolume.value;
        GameManager.Instance.SettingsManager.UpdateSettingsData(settings);
    }

    public void OnUIChange()
    {
        ApplySettings();
    }

    #endregion

}
