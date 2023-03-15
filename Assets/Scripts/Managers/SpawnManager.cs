using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private SpawnManagerData _spawnManagerData;

    [SerializeField]
    private PlayerControllerBehaviour _target;

    [SerializeField]
    private CurveValue _difficultyCurve;

    [SerializeField]
    private TimerController _timer;

    [SerializeField]
    private GameplayManager _gameplayManager;

    [Header("Generated Data")]
    [SerializeField]
    private float _borderWidth;

    [SerializeField]
    private float _borderHeight;

    [Header("Additional")]
    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    public virtual void Init(PlayerControllerBehaviour target,
        SpawnManagerData spawnManagerData,
        int borderWidth, int borderHeight,
        Logger logger)
    {
        _target = target;
        _spawnManagerData = spawnManagerData;
        _borderWidth = (float)borderWidth - _spawnManagerData.BorderOffset;
        _borderHeight = (float)borderHeight - _spawnManagerData.BorderOffset;

        _difficultyCurve = new CurveValue();
        _difficultyCurve.Init(_spawnManagerData.DifficultyCurve,
            _spawnManagerData.MinSpawnInterval * _gameplayManager.GetGameplayDifficulty().SpawnIntervalMultiply,
            _spawnManagerData.MaxSpawnInterval * _gameplayManager.GetGameplayDifficulty().SpawnIntervalMultiply,
            _spawnManagerData.CurveTimeMinutes);
        _timer = GameManager.Instance.CurrentLevelManager.TimerController;
        _logger = logger;

        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(_spawnManagerData.SpawnStartTime);

        while (!_target.HealthBehaviour.IsDead)
        {
            var WaveChance = Random.Range(0, 1f);
            if (WaveChance <= _spawnManagerData.WaveSpawnChance * _gameplayManager.GetGameplayDifficulty().WaveSpawnChanceMultiply)
            {
                yield return StartCoroutine(SpawnWaveWithChance());
            }
            else
            {
                SpawnSingleEnemy();
                _logger.Log($"Enemy spawned", this);
            }

            var time = _difficultyCurve.GetValue(_timer.Elapsedtime);
            _logger.Log($"{time} before next spawn", this);

            yield return new WaitForSeconds(time);
        }
    }

    private void SpawnSingleEnemy()
    {
        Vector2 spawnPosition = new Vector2();
        do
        {
            spawnPosition = Random.insideUnitCircle * _spawnManagerData.MaxSpawnRadius + (Vector2)_target.transform.position;
        } while (spawnPosition.x < _spawnManagerData.BorderOffset || spawnPosition.x > _borderWidth ||
                    spawnPosition.y < _spawnManagerData.BorderOffset || spawnPosition.y > _borderHeight ||
                    Vector2.Distance(spawnPosition, _target.transform.position) <= _spawnManagerData.MinSpawnRadius);

        var enemyIndex = Random.Range(0, _spawnManagerData.EnemiesPrefabs.Count);
        Instantiate(_spawnManagerData.EnemiesPrefabs[enemyIndex], spawnPosition, Quaternion.identity, transform)
            .Init(_target);
    }

    private IEnumerator SpawnWaveWithChance()
    {
        var enemiesCount = _spawnManagerData.MaxCountInWave * _difficultyCurve.GetCurve(_timer.Elapsedtime);
        _logger.Log($"Wave with {enemiesCount} enemies started", this);
        for (int i = 0; i < enemiesCount; i++)
        {
            SpawnSingleEnemy();
            yield return new WaitForSeconds(0.3f);
        }
    }

    #endregion

}
