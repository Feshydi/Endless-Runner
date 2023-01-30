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
    private EnemySpawner _spawner;

    [SerializeField]
    private PlayerController _player;

    #endregion

    #region Methods

    public void CreateLevel(LevelData levelData)
    {
        _groundMap.Init(levelData.TerrainMapData);
        _perimeterBuilder.Init(levelData.TerrainMapData);
        _spawner.Init(_player, levelData.EnemyList[0]);
    }

    #endregion

}
