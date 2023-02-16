using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class SettingsManager : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private Settings _settings;

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

        DontDestroyOnLoad(gameObject);
    }

    public bool LoadData()
    {
        if (!File.Exists(_savePath))
        {
            File.Create(_savePath).Dispose();
            return false;
        }

        using (StreamReader stream = new StreamReader(_savePath))
        {
            string json = stream.ReadToEnd();
            _settings = JsonConvert.DeserializeObject<Settings>(json);

            if (_settings.IsNull())
                return false;
        }

        return true;
    }

    public void SaveData()
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
    }

    #endregion

}
