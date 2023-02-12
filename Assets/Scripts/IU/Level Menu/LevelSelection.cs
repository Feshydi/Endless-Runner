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
    [SerializeField]
    private Transform _contextHolderTransform;

    [SerializeField]
    private List<LevelData> _levelDatas;

    [SerializeField]
    private Button _levelPrefab;

    [SerializeField]
    private Toggle _toggle;

    [SerializeField]
    private TMP_InputField _inputField;

    [SerializeField]
    private Button _playButton;

    [Header("Generated")]
    [SerializeField]
    private LevelData _selectedLevel;

    [SerializeField]
    private Button _selectedLevelButton;

    [Header("Additional")]
    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void Awake()
    {
        _selectedLevel = null;
        _selectedLevelButton = null;

        CreateLevelSelectors();
    }

    private void CreateLevelSelectors()
    {
        foreach (var levelData in _levelDatas)
        {
            var level = Instantiate(_levelPrefab, _contextHolderTransform);
            level.GetComponent<Level>().Init(this, levelData);

            if (_selectedLevel == null)
                SelectLevel(level, levelData);
        }
    }

    public void SelectLevel(Button levelButton, LevelData levelData)
    {
        if (levelButton == null || levelData == null)
        {
            _logger.Log("Unexpected error when trying to select level", this);
            return;
        }

        if (_selectedLevelButton != null)
        {
            _selectedLevelButton.interactable = true;
        }

        _selectedLevelButton = levelButton;
        _selectedLevel = levelData;

        GameManager.Instance.LevelData = _selectedLevel;

        _selectedLevelButton.interactable = false;
        _playButton.interactable = true;
        _playButton.Select();

        _logger.Log($"Level {_selectedLevel} selected", this);
    }

    public void LoadLevel()
    {
        if (_selectedLevel == null)
        {
            _logger.Log("No level selected", this);
            return;
        }

        GameManager.Instance.LevelData.AutoSeedGeneration = _toggle.isOn;
        if (!_toggle.isOn)
            GameManager.Instance.LevelData.Seed = int.Parse(_inputField.text);

        GameManager.Instance.LoadingManager.LoadLevel();
    }

    #endregion

}
