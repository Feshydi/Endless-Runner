using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileDirection
{
    UpLeftCorner, Up, UpRightCorner,
    Left, Central, Right,
    DownLeftCorner, Down, DownRightCorner
}

[System.Serializable]
public struct DirectionalTiles
{
    [SerializeField]
    private Tile _tile;

    [SerializeField]
    private TileDirection _tileDirection;

    public Tile Tile => _tile;

    public TileDirection TileDirection => _tileDirection;
}

[CreateAssetMenu]
public class TileDirectionData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private List<DirectionalTiles> _directionalTiles;

    #endregion

    #region Properties

    public List<DirectionalTiles> DirectionalTiles => _directionalTiles;

    #endregion

    #region Methods

    public Tile GetDirectionTile(TileDirection tileDirection)
    {
        foreach (var tile in _directionalTiles)
        {
            if (tile.TileDirection.Equals(tileDirection))
                return tile.Tile;
        }
        return null;
    }

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
