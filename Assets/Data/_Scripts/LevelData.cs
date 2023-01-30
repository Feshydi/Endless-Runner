using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level")]
public class LevelData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private MapData _terrainMapData;

    [SerializeField]
    private List<EnemyController> _enemyList;

    #endregion

    #region Properties

    public MapData TerrainMapData
    {
        get => _terrainMapData;
        set => _terrainMapData = value;
    }

    public List<EnemyController> EnemyList => _enemyList;

    #endregion

}
