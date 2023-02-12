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

    [SerializeField]
    private bool _autoSeedGeneration = true;

    [SerializeField]
    private int _seed;

    #endregion

    #region Properties

    public string Name => _name;

    public Sprite Icon => _icon;

    public MapData TerrainMapData => _terrainMapData;

    public bool AutoSeedGeneration
    {
        get => _autoSeedGeneration;
        set => _autoSeedGeneration = value;
    }

    public int Seed
    {
        get => _seed;
        set => _seed = value;
    }

    #endregion

}
