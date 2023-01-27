using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#region Structures

public struct TileVector
{
    public Vector3Int Vector;

    public Tile Tile;

    public TileVector(Vector3Int vector, Tile tile)
    {
        Vector = vector;
        Tile = tile;
    }
}

#endregion

public class PerlinNoiseMap : MonoBehaviour
{

    #region Fields

    [Header("Data for generation")]
    [SerializeField]
    private MapData _mapData;

    [SerializeField]
    private int _seed;

    [SerializeField]
    private bool _autoSeedGeneration;

    [Header("Generated data")]
    [SerializeField]
    private float[,] _noiseMap;

    [SerializeField]
    private List<TileVector> _tileVector;

    [SerializeField]
    private Tilemap _tilemap;

    [Header("Additional")]
    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    /// <summary>
    /// Generates with Perlin noise method structure using passed parameters.
    /// </summary>
    /// <param name="mapData"></param>
    public void Init(MapData mapData)
    {
        _mapData = mapData;

        if (_autoSeedGeneration)
            _seed = Random.Range(0, int.MaxValue);

        Random.InitState(_seed);
        _noiseMap = new float[_mapData.MapWidth, _mapData.MapHeight];
        _tileVector = new List<TileVector>();
        _tilemap = GetComponent<Tilemap>();

        GenerateNoiseMap();
        GenerateTileMap();
        SetTileMap();
    }

    /// <summary>
    /// Simply generates Perlin noise map.
    /// </summary>
    private void GenerateNoiseMap()
    {
        var offsetX = Random.Range(-9999f, 9999f);
        var offsetY = Random.Range(-9999f, 9999f);

        for (int x = 0; x < _mapData.MapWidth; x++)
        {
            for (int y = 0; y < _mapData.MapHeight; y++)
            {
                var scaledX = (float)x / _mapData.MapWidth * _mapData.Scale + offsetX;
                var scaledY = (float)y / _mapData.MapHeight * _mapData.Scale + offsetY;

                var perlinValue = Mathf.PerlinNoise(scaledX, scaledY);
                _noiseMap[x, y] = Mathf.Clamp(perlinValue, 0, 1);
            }
        }
        _logger.Log($"Noisemap for tilemap {_tilemap.name} generated", this);
    }

    /// <summary>
    /// Creates list of tiles and its position, based on Perlin noise map.
    /// </summary>
    private void GenerateTileMap()
    {
        for (int x = 0; x < _mapData.MapWidth; x++)
        {
            for (int y = 0; y < _mapData.MapHeight; y++)
            {
                var selectedTile = GetTile(_noiseMap[x, y]);
                _tileVector.Add(new TileVector(new Vector3Int(x, y), selectedTile));
            }
        }
        _logger.Log($"Tilemap {_tilemap.name} generated", this);
    }

    /// <summary>
    /// <c>GetTile</c> gets the random tile by its initial chances.
    /// </summary>
    private Tile GetTile(float perlinValue)
    {
        float chanceValue = 0f;
        foreach (var tileChance in _mapData.TerrainTileData)
        {
            if (perlinValue >= chanceValue && perlinValue <= chanceValue + tileChance.InitialChance)
            {
                var tiles = tileChance.TileDirectionData.DirectionalTiles;
                return tiles[Random.Range(0, tiles.Count)].Tile;
            }
            chanceValue += tileChance.InitialChance;
        }
        return null;
    }

    /// <summary>
    /// Set tiles to its position on the grid.
    /// </summary>
    private void SetTileMap()
    {
        foreach (var tile in _tileVector)
        {
            if (tile.Tile == null || tile.Vector == null)
            {
                _logger.Log($"Tilemap {_tilemap.name} contain empty tiles or coordinates", this);
                return;
            }
        }
        foreach (var tile in _tileVector)
        {
            _tilemap.SetTile(tile.Vector, tile.Tile);
        }
        _logger.Log($"Tilemap {_tilemap.name} applied to scene", this);
    }

    //public Tile GetDirectionTile(TileDirection tileDirection)
    //{
    //    foreach (var tile in _directionalTiles)
    //    {
    //        if (tile.TileDirection.Equals(tileDirection))
    //            return tile.Tile;
    //    }
    //    return null;
    //}

    //public TileDirection GetTileDirection(float[,] array, int width, int height)
    //{
    //    switch (array)
    //    {
    //        case var value when< [width, height]:

    //            break;

    //    }

    //    return TileDirection.Central;
    //}

    #endregion

}
