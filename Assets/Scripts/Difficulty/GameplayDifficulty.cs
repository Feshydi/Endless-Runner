using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Difficulties/Gameplay Difficulty")]
public class GameplayDifficulty : ScriptableObject
{

    #region Fields

    [SerializeField] private Difficulty _difficulty;

    [Header("Difficulty Multipliers")]
    [SerializeField] private float _spawnIntervalMultiply;
    [SerializeField] private float _waveSpawnChanceMultiply;

    [SerializeField] private float _enemyHealthMultiply;
    [SerializeField] private float _enemyMaxMoveSpeedMultiply;
    [SerializeField] private float _enemyDamageMultiply;
    [SerializeField] private float _enemyDamageRateMultiply;

    [SerializeField] private float _explosionCooldownMultiply;
    [SerializeField] private float _burstCooldownMultiply;

    #endregion

    #region Properties

    public Difficulty Difficulty => _difficulty;

    public float SpawnIntervalMultiply => _spawnIntervalMultiply;
    public float WaveSpawnChanceMultiply => _waveSpawnChanceMultiply;

    public float EnemyHealthMultiply => _enemyHealthMultiply;
    public float EnemyMaxMoveSpeedMultiply => _enemyMaxMoveSpeedMultiply;
    public float EnemyDamageMultiply => _enemyDamageMultiply;
    public float EnemyDamageRateMultiply => _enemyDamageRateMultiply;

    public float ExplosionCooldownMultiply => _explosionCooldownMultiply;
    public float BurstCooldownMultiply => _burstCooldownMultiply;

    #endregion

}
