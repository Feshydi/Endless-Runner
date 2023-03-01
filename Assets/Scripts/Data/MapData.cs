using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#region Structures

[System.Serializable]
public struct TileData
{
    [SerializeField]
    private TilePackData _tileDirectionData;

    [SerializeField]
    [Range(0.01f, 1.00f)]
    private float _initialChance;

    public TilePackData TileDirectionData => _tileDirectionData;

    public float InitialChance => _initialChance;
}

#endregion

[CreateAssetMenu(menuName = "Data/Map")]
public class MapData : ScriptableObject
{

    #region Fields

    [Header("Terrain tiles")]
    [SerializeField]
    private List<TileData> _terrainTileData;

    [Header("Wall tiles")]
    [SerializeField]
    private TilePackData _wallTilePackData;

    [Header("Map Generator Settings")]
    [SerializeField]
    private int _mapWidth;

    [SerializeField]
    private int _mapHeight;

    [SerializeField]
    private int _scale;

    #endregion

    #region Properties

    public List<TileData> TerrainTileData => _terrainTileData;

    public TilePackData WallTilePackData => _wallTilePackData;

    public int MapWidth => _mapWidth;

    public int MapHeight => _mapHeight;

    public int Scale => _scale;

    #endregion

}
