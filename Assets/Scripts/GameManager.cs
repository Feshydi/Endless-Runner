using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private LevelData _levelData;

    [Header("Setting")]
    [SerializeField]
    private LevelLauncher _levelLauncher;

    #endregion

    #region Properties

    public LevelData LevelData
    {
        get => _levelData;
        set => _levelData = value;
    }

    #endregion

    #region Methods

    private void Start()
    {
        _levelLauncher.CreateLevel(_levelData);
    }

    #endregion

}
