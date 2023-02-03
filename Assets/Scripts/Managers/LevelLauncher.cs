using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLauncher : MonoBehaviour
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

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    public void CreateLevel(LevelData levelData)
    {
        _groundMap.Init(levelData.TerrainMapData);
        _perimeterBuilder.Init(levelData.TerrainMapData);

        var spawnManager = Instantiate(_spawnManagerPrefab, transform);
        spawnManager.Init(_player, _spawnManagerData, levelData.TerrainMapData.MapWidth, levelData.TerrainMapData.MapHeight, _logger);

        _logger.Log($"{gameObject} completed, level created", this);
    }

    #endregion

}
