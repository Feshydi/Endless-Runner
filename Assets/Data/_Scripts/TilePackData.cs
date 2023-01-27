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

[CreateAssetMenu(menuName = "Data/Tile Pack")]
public class TilePackData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private List<DirectionalTiles> _directionalTiles;

    #endregion

    #region Properties

    public List<DirectionalTiles> DirectionalTiles => _directionalTiles;

    #endregion

}
