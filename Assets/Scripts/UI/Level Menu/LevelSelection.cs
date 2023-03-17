using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSelection : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField] private LoadedLevelData _loadedLevelData;
    [SerializeField] private List<LevelUI> _levels;
    [SerializeField] private int _selectedLevelIndex;

    [Header("Init")]
    [SerializeField] private Transform _levelsHolder;
    [SerializeField] private List<LevelData> _levelsData;
    [SerializeField] private LevelUI _levelPrefab;

    [Header("Seed")]
    [SerializeField] private Toggle _seedToggle;
    [SerializeField] private TMP_InputField _seedInputField;

    [Header("Additional")]
    [SerializeField] private Logger _logger;

    #endregion

    #region Methods

    private void Start()
    {
        CreateLevelSelectors();
        SetDefaultLevel();
    }

    private void CreateLevelSelectors()
    {
        for (int i = 0; i < _levelsData.Count; i++)
        {
            var level = Instantiate(_levelPrefab, _levelsHolder);
            level.GetComponent<LevelUI>().Init(_levelsData[i]);
            _levels.Add(level);

            level.gameObject.SetActive(false);
        }

        if (_levels.Count <= 0)
        {
            _logger.Log("Zero Levels initialized", this);
            Destroy(gameObject);
        }
    }

    private void SetDefaultLevel()
    {
        _loadedLevelData.LevelData = _levelsData[0];
        _levels[0].gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        _levels[_selectedLevelIndex].gameObject.SetActive(false);
        _selectedLevelIndex = (_selectedLevelIndex + 1) % _levels.Count;
        _levels[_selectedLevelIndex].gameObject.SetActive(true);
        _loadedLevelData.LevelData = _levelsData[_selectedLevelIndex];
    }

    public void PreviousLevel()
    {
        _levels[_selectedLevelIndex].gameObject.SetActive(false);
        _selectedLevelIndex--;
        if (_selectedLevelIndex < 0)
            _selectedLevelIndex += _levels.Count;
        _levels[_selectedLevelIndex].gameObject.SetActive(true);
        _loadedLevelData.LevelData = _levelsData[_selectedLevelIndex];
    }

    public void LoadLevel()
    {
        SetSeed();
        GameManager.Instance.LoadingManager.LoadLevel();
    }

    private void SetSeed()
    {
        _loadedLevelData.AutoSeedGeneration = _seedToggle.isOn;
        if (!_seedToggle.isOn)
            _loadedLevelData.Seed = int.Parse(_seedInputField.text);
    }

    #endregion

}
