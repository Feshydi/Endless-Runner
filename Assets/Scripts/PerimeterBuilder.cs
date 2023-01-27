using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PerimeterBuilder : MonoBehaviour
{

    #region Fields

    [Header("Data for generation")]
    [SerializeField]
    private MapData _mapData;

    [Header("Generated data")]
    [SerializeField]
    private Tilemap _tilemap;

    [Header("Additional")]
    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    public void Init(MapData mapData)
    {
        _mapData = mapData;
        _tilemap = GetComponent<Tilemap>();

        GeneratePerimeter();
    }

    /// <summary>
    /// Generates perimeter walls.
    /// </summary>
    void GeneratePerimeter()
    {
        var tiles = _mapData.WallTilePackData.DirectionalTiles;

        // Create the top and bottom rows of tiles
        for (int i = 0; i <= _mapData.MapWidth; i++)
        {
            var tile = GetRandomTile(tiles);

            _tilemap.SetTile(new Vector3Int(i, 0), tile);
            _tilemap.SetTile(new Vector3Int(i, _mapData.MapHeight), tile);
        }

        // Create the left and right columns of tiles
        for (int i = 1; i <= _mapData.MapHeight - 1; i++)
        {
            var tile = GetRandomTile(tiles);

            _tilemap.SetTile(new Vector3Int(0, i), tile);
            _tilemap.SetTile(new Vector3Int(_mapData.MapWidth, i), tile);
        }

        _logger.Log($"Wall perimeter for {_tilemap.name} generated", this);
    }

    /// <summary>
    /// Gets random tile from list.
    /// </summary>
    /// <param name="tiles"></param>
    /// <returns></returns>
    private Tile GetRandomTile(List<DirectionalTiles> tiles)
    {
        var tileIndex = Random.Range(0, tiles.Count);
        return tiles[tileIndex].Tile;
    }

    #endregion

}
