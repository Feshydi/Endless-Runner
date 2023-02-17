using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private Settings _settings;

    [SerializeField]
    private AudioMixerGroup _mixer;

    [SerializeField]
    private string _savePath;

    #endregion

    #region Properties

    public Settings Settings => _settings;

    #endregion

    #region Methods

    public void Init()
    {
        _savePath = DataPath.Settings;
        if (!LoadData())
            _settings.SetDefaultValues();

        ChangeVolume();

        DontDestroyOnLoad(gameObject);
    }

    private bool LoadData()
    {
        if (!File.Exists(_savePath))
        {
            File.Create(_savePath).Dispose();
            return false;
        }

        using (StreamReader stream = new StreamReader(_savePath))
        {
            string json = stream.ReadToEnd();
            if (string.IsNullOrEmpty(json))
                return false;

            try
            {
                _settings = JsonConvert.DeserializeObject<Settings>(json);
            }
            catch (JsonReaderException)
            {
                return false;
            }
        }

        return true;
    }

    private void SaveData()
    {
        using (StreamWriter stream = new StreamWriter(_savePath))
        {
            string json = JsonConvert.SerializeObject(_settings);
            stream.Write(json);
        }
    }

    public void UpdateSettingsData(Settings settings)
    {
        _settings = settings;
        SaveData();
        LoadData();

        ChangeVolume();
    }

    private void ChangeVolume()
    {
        float _minVolume = -80f;
        float _maxVolume = 0f;
        _mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(_minVolume, _maxVolume, _settings.MasterVolume));
        _mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(_minVolume, _maxVolume, _settings.MusicVolume));
        _mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(_minVolume, _maxVolume, _settings.EffectsVolume));
    }

    #endregion

}
