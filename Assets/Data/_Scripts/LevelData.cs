using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level")]
public class LevelData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private MapData _terrainMapData;

    #endregion

    #region Properties

    public MapData TerrainMapData => _terrainMapData;

    #endregion

}
