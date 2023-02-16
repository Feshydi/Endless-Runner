using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level")]
public class LevelData : ScriptableObject
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private string _name = "Level 1";

    [SerializeField]
    private Sprite _icon;

    [Header("World Settings")]
    [SerializeField]
    private MapData _terrainMapData;

    #endregion

    #region Properties

    public string Name => _name;

    public Sprite Icon => _icon;

    public MapData TerrainMapData => _terrainMapData;

    #endregion

}
