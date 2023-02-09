using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    protected SpawnManagerData _spawnManagerData;

    [SerializeField]
    protected PlayerController _target;

    [Header("Generated Data")]
    [SerializeField]
    protected float _borderWidth;

    [SerializeField]
    protected float _borderHeight;

    [SerializeField]
    protected float _leftFOVBorder;

    [SerializeField]
    protected float _rightFOVBorder;

    [SerializeField]
    protected float _downFOVBorder;

    [SerializeField]
    protected float _upFOVBorder;

    [Header("Additional")]
    [SerializeField]
    protected Logger _logger;

    #endregion

    #region Methods

    public virtual void Init(PlayerController target, SpawnManagerData spawnManagerData, int borderWidth, int borderHeight, Logger logger)
    {
        _target = target;
        _spawnManagerData = spawnManagerData;
        _borderWidth = (float)borderWidth - _spawnManagerData.BorderOffset;
        _borderHeight = (float)borderHeight - _spawnManagerData.BorderOffset;

        _logger = logger;

        Invoke("SpawnEnemy", _spawnManagerData.SpawnStartTime);
    }

    protected virtual void SpawnEnemy()
    {
        if (_target.IsDead)
            return;

        Vector2 spawnPosition = new Vector2();
        do
        {
            spawnPosition = Random.insideUnitCircle * _spawnManagerData.MaxSpawnRadius + (Vector2)_target.transform.position;
        } while (spawnPosition.x < _spawnManagerData.BorderOffset || spawnPosition.x > _borderWidth ||
                    spawnPosition.y < _spawnManagerData.BorderOffset || spawnPosition.y > _borderHeight ||
                    Vector2.Distance(spawnPosition, _target.transform.position) <= _spawnManagerData.MinSpawnRadius);

        var enemyIndex = Random.Range(0, _spawnManagerData.EnemiesPrefabs.Count);
        var enemy = Instantiate(_spawnManagerData.EnemiesPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
        enemy.Init(_target);

        _logger.Log($"{gameObject} spawned {enemy.gameObject}", this);

        Invoke("SpawnEnemy", _spawnManagerData.SpawnInterval);
    }

    #endregion

}
