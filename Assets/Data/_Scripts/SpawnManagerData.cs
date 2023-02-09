using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Spawn Manager")]
public class SpawnManagerData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private List<EnemyController> _enemiesPrefabs;

    [SerializeField]
    protected float _borderOffset;

    [SerializeField]
    private float _minSpawnRadius;

    [SerializeField]
    private float _maxSpawnRadius;

    [SerializeField]
    private float _spawnStartTime;

    [SerializeField]
    private float _spawnInterval;

    #endregion

    #region Properties

    public List<EnemyController> EnemiesPrefabs => _enemiesPrefabs;

    public float BorderOffset => _borderOffset;

    public float MinSpawnRadius => _minSpawnRadius;

    public float MaxSpawnRadius => _maxSpawnRadius;

    public float SpawnStartTime => _spawnStartTime;

    public float SpawnInterval => _spawnInterval;

    #endregion

}
