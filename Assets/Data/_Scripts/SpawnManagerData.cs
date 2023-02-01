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
    private float _spawnRadius;

    [SerializeField]
    private float _spawnStartTime;

    [SerializeField]
    private float _spawnInterval;

    #endregion

    #region Properties

    public List<EnemyController> EnemiesPrefabs => _enemiesPrefabs;

    public float SpawnRadius => _spawnRadius;

    public float SpawnStartTime => _spawnStartTime;

    public float SpawnInterval => _spawnInterval;

    #endregion

}
