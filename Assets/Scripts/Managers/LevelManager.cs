using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    #region Fields

    [Header("Map")]
    [SerializeField]
    private PerlinNoiseMap _groundMap;

    [SerializeField]
    private PerimeterBuilder _perimeterBuilder;

    [SerializeField]
    private PerlinNoiseMap _objectMap;

    [Header("Entities")]
    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private SpawnManager _spawnManagerPrefab;

    [SerializeField]
    private SpawnManagerData _spawnManagerData;

    [Header("Additional")]
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
        var gameManager = GameManager.Instance;
        gameManager.CurrentLevelManager = this;

        var levelData = gameManager.LevelData;
        if (levelData == null)
        {
            _logger.Log("No Level Data", this);
            return;
        }

        if (gameManager.AutoSeedGeneration)
            gameManager.Seed = Random.Range(0, int.MaxValue);
        Random.InitState(gameManager.Seed);

        _groundMap.Init(levelData.TerrainMapData);
        _perimeterBuilder.Init(levelData.TerrainMapData);

        InitPlayer(levelData);

        Instantiate(_spawnManagerPrefab, transform)
            .Init(_player, _spawnManagerData, levelData.TerrainMapData.MapWidth, levelData.TerrainMapData.MapHeight, _logger);

        gameManager.ScoreManager.ResetScore();
        TimerController.Init();

        _logger.Log($"{gameObject} completed, level created", this);
    }

    private void InitPlayer(LevelData levelData)
    {
        var offset = 15f;
        var height = Random.Range(offset, levelData.TerrainMapData.MapHeight - offset);
        var width = Random.Range(offset, levelData.TerrainMapData.MapHeight - offset);
        _player.transform.position = new Vector2(height, width);
    }

    private void OnDestroy()
    {
        GameManager.Instance.CurrentLevelManager = null;
    }

    #endregion

}
