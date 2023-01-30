using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private EnemyController _enemyPrefab;

    [SerializeField]
    private float _spawnRadius = 5f;

    [SerializeField]
    private float _spawnInterval = 3f;

    [Header("Additional")]
    [SerializeField]
    private Logger _logger;

    #endregion

    public void Init(PlayerController player, EnemyController enemy)
    {
        _player = player;
        _enemyPrefab = enemy;

        InvokeRepeating("SpawnWave", _spawnInterval, _spawnInterval);
    }

    private void SpawnWave()
    {
        Vector2 spawnPosition = Random.insideUnitCircle * _spawnRadius + (Vector2)_player.transform.position;

        var enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
        enemy.Init(_player);

        _logger.Log("Wave spawned", this);
    }
}
