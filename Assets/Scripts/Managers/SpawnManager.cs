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

    [SerializeField]
    private int _borderWidth;

    [SerializeField]
    private int _borderHeight;

    [Header("Additional")]
    [SerializeField]
    protected Logger _logger;

    #endregion

    #region Methods

    public virtual void Init(PlayerController target, SpawnManagerData spawnManagerData, int borderWidth, int borderHeight, Logger logger)
    {
        _target = target;
        _spawnManagerData = spawnManagerData;
        _borderWidth = borderWidth;
        _borderHeight = borderHeight;

        _logger = logger;

        InvokeRepeating("SpawnEnemy", _spawnManagerData.SpawnStartTime, _spawnManagerData.SpawnInterval);
    }

    protected virtual void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2();
        do
        {
            spawnPosition = Random.insideUnitCircle * _spawnManagerData.SpawnRadius + (Vector2)_target.transform.position;
        } while (spawnPosition.x < 1 || spawnPosition.x > _borderWidth - 1 ||
                    spawnPosition.y < 1 || spawnPosition.y > _borderHeight - 1);

        var enemyIndex = Random.Range(0, _spawnManagerData.EnemiesPrefabs.Count);
        var enemy = Instantiate(_spawnManagerData.EnemiesPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
        enemy.Init(_target);

        _logger.Log($"{gameObject} spawned {enemy.gameObject}", this);
    }

    #endregion

}
