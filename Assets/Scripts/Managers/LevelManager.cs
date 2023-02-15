using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private PerlinNoiseMap _groundMap;

    [SerializeField]
    private PerimeterBuilder _perimeterBuilder;

    [SerializeField]
    private PerlinNoiseMap _objectMap;

    [SerializeField]
    private SpawnManager _spawnManagerPrefab;

    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private SpawnManagerData _spawnManagerData;

    public TimerController TimerController;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        GameManager.Instance.CurrentLevelManager = this;

        var levelData = GameManager.Instance.LevelData;
        if (levelData == null)
        {
            _logger.Log("No Level Data", this);
            return;
        }

        if (levelData.AutoSeedGeneration)
            levelData.Seed = Random.Range(0, int.MaxValue);
        Random.InitState(levelData.Seed);

        _groundMap.Init(levelData.TerrainMapData);
        _perimeterBuilder.Init(levelData.TerrainMapData);

        Instantiate(_spawnManagerPrefab, transform)
            .Init(_player, _spawnManagerData, levelData.TerrainMapData.MapWidth, levelData.TerrainMapData.MapHeight, _logger);

        GameManager.Instance.ScoreManager.ResetScore();
        TimerController.Init();

        _logger.Log($"{gameObject} completed, level created", this);
    }

    private void OnDestroy()
    {
        GameManager.Instance.CurrentLevelManager = null;
    }

    #endregion

}
