using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private LoadedLevelData _loadedLevelData;

    [SerializeField]
    private GameplayManager _gameplayManager;

    [Header("Map")]
    [SerializeField]
    private PerlinNoiseMap _groundMap;

    [SerializeField]
    private PerimeterBuilder _perimeterBuilder;

    [SerializeField]
    private PerlinNoiseMap _objectMap;

    [Header("Entities")]
    public PlayerControllerBehaviour Player;

    [SerializeField]
    private SpawnManager _spawnManagerPrefab;

    [SerializeField]
    private List<SpawnManagerData> _spawnManagersData;

    [Header("Additional")]
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private PlayerUIManager _playerUIManager;

    public TimerController TimerController;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void Start()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        var gameManager = GameManager.Instance;
        gameManager.CurrentLevelManager = this;

        var levelData = _loadedLevelData.LevelData;
        if (levelData == null)
        {
            _logger.Log("No Level Data", this);
            return;
        }

        if (_loadedLevelData.AutoSeedGeneration)
            _loadedLevelData.Seed = Random.Range(0, int.MaxValue);
        Random.InitState(_loadedLevelData.Seed);

        _groundMap.Init(levelData.TerrainMapData);
        _perimeterBuilder.Init(levelData.TerrainMapData);

        _gameplayManager.InitModes();

        InitPlayer(levelData);
        _playerUIManager.Init(Player);

        InitSpawners(levelData);

        gameManager.ScoreManager.ResetScore();
        TimerController.Init();

        _logger.Log($"{gameObject} completed, level created", this);
    }

    private void InitPlayer(LevelData levelData)
    {
        Player = Instantiate(_loadedLevelData.Player, transform.position, Quaternion.identity, transform.parent);
        var offset = 15f;
        var height = Random.Range(offset, levelData.TerrainMapData.MapHeight - offset);
        var width = Random.Range(offset, levelData.TerrainMapData.MapHeight - offset);
        Player.transform.position = new Vector2(height, width);
        Player.Camera = _camera;
        Player.GetComponent<CameraFollow>().Map = _groundMap.GetComponent<Tilemap>();
    }

    private void InitSpawners(LevelData levelData)
    {
        foreach (var spawnerData in _spawnManagersData)
        {
            Instantiate(_spawnManagerPrefab, transform)
                .Init(Player, spawnerData,
                levelData.TerrainMapData.MapWidth, levelData.TerrainMapData.MapHeight,
                _logger);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.CurrentLevelManager = null;
        Destroy(Player.gameObject);
    }

    #endregion

}
