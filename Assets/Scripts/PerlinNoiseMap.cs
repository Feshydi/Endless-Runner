using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#region Structures

[System.Serializable]
public struct TileData
{
    [SerializeField]
    private TileDirectionData _tileDirectionData;

    [SerializeField]
    [Range(0.01f, 1.00f)]
    private float _initialChance;

    public TileDirectionData TileDirectionData => _tileDirectionData;

    public float InitialChance => _initialChance;
}

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

    #region Properties

    [Header("List of tile sets")]
    [SerializeField]
    private List<TileData> _tileData;

    [Header("Map Generator Settings")]
    [SerializeField]
    private int _mapWidth;

    [SerializeField]
    private int _mapHeight;

    [SerializeField]
    private int _scale;

    [SerializeField]
    private int _seed;

    [Header("Additional")]
    [SerializeField]
    private Logger _logger;

    private float[,] _noiseMap;

    private List<TileVector> _tileVector;

    private Tilemap _tilemap;

    #endregion

    #region Methods

    private void Awake()
    {
        _noiseMap = new float[_mapWidth, _mapHeight];
        _tileVector = new List<TileVector>();
        _tilemap = GetComponent<Tilemap>();
    }

    private void Start()
    {
        // simply generate Perlin noise map
        GenerateNoiseMap();
        // create list of tiles and vectors, based on Perlin noise map and initial chances
        GenerateTileMap();
        // set tiles to its position
        SetTileMap();
    }

    private void GenerateNoiseMap()
    {
        Random.InitState(_seed);
        var offsetX = Random.Range(-9999f, 9999f);
        var offsetY = Random.Range(-9999f, 9999f);

        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                var scaledX = (float)x / _mapWidth * _scale + offsetX;
                var scaledY = (float)y / _mapHeight * _scale + offsetY;

                var perlinValue = Mathf.PerlinNoise(scaledX, scaledY);
                _noiseMap[x, y] = Mathf.Clamp(perlinValue, 0, 1);
            }
        }
        _logger.Log("Generated noise map", this);
    }

    private void GenerateTileMap()
    {
        for (int x = 0; x < _mapWidth; x++)
        {
            for (int y = 0; y < _mapHeight; y++)
            {
                var selectedTile = GetTile(_noiseMap[x, y]);
                _tileVector.Add(new TileVector(new Vector3Int(x, y), selectedTile));
            }
        }
        _logger.Log("Generated tile map", this);
    }

    private Tile GetTile(float perlinValue)
    {
        float chanceValue = 0f;
        foreach (var tileChance in _tileData)
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

    private void SetTileMap()
    {
        foreach (var tile in _tileVector)
        {
            if (tile.Tile == null || tile.Vector == null)
            {
                _logger.Log($"Tilemap contain empty tiles or coordinates", this);
                return;
            }
        }
        foreach (var tile in _tileVector)
        {
            _tilemap.SetTile(tile.Vector, tile.Tile);
        }
        _logger.Log("Tilemap applied to scene", this);
    }

    #endregion

}
