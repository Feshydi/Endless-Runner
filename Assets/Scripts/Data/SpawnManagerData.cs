using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Spawn Manager")]
public class SpawnManagerData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private List<EnemyController> _enemiesPrefabs;

    [Header("Position Data")]
    [SerializeField]
    protected float _borderOffset;

    [SerializeField]
    private float _minSpawnRadius;

    [SerializeField]
    private float _maxSpawnRadius;

    [Header("Time Data")]
    [SerializeField]
    private float _spawnStartTime;

    [SerializeField]
    private AnimationCurve _difficultyCurve;

    [SerializeField]
    private float _curveTimeMinutes;

    [SerializeField]
    private float _minSpawnInterval;

    [SerializeField]
    private float _maxSpawnInterval;

    [Header("Wave Data")]
    [SerializeField]
    [Range(0, 1)]
    private float _waveSpawnChance;

    [SerializeField]
    private int _maxCountInWave;

    #endregion

    #region Properties

    public List<EnemyController> EnemiesPrefabs => _enemiesPrefabs;

    public float BorderOffset => _borderOffset;

    public float MinSpawnRadius => _minSpawnRadius;

    public float MaxSpawnRadius => _maxSpawnRadius;

    public float SpawnStartTime => _spawnStartTime;

    public AnimationCurve DifficultyCurve => _difficultyCurve;

    public float CurveTimeMinutes => _curveTimeMinutes;

    public float MinSpawnInterval => _minSpawnInterval;

    public float MaxSpawnInterval => _maxSpawnInterval;

    public float WaveSpawnChance => _waveSpawnChance;

    public int MaxCountInWave => _maxCountInWave;

    #endregion

}
