using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyController : EnemyController
{

    #region Fields

    [Header("Circular Moving")]
    [SerializeField]
    private float _changeDirectionTime;

    [SerializeField]
    private float _angularSpeedRange;

    [SerializeField]
    private float _angularSpeed;

    private float _angle;

    [Header("Shooting")]
    [SerializeField]
    private EnemyProjectile _projectilePrefab;

    [SerializeField]
    private float _shootDistance;

    private float _nextShootTime;

    [SerializeField]
    private AudioSource _shootSound;

    #endregion

    #region Methods

    protected override void Start()
    {
        base.Start();
        StartCoroutine(ChangeRotateDirection());
    }

    private void Update()
    {
        if (_targetHealth is null || _targetHealth.IsDead)
            return;

        if (Time.time > _nextShootTime &&
            (_rigidbody2D.position - (Vector2)_targetHealth.transform.position).magnitude <= _shootDistance)
        {
            _nextShootTime = Time.time + 60 / (_enemyData.DamageRate * _gameplayManager.GetGameplayDifficulty().EnemyDamageMultiply);
            ShootHandle();
        }
    }

    private void FixedUpdate()
    {
        MoveCircular();
    }

    private IEnumerator ChangeRotateDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(_changeDirectionTime);
            _angularSpeed = Random.Range(-_angularSpeedRange, _angularSpeedRange);
        }
    }

    private void MoveCircular()
    {
        if (_healthBehaviour.IsDead || _healthBehaviour.IsHitted)
            return;

        if (_targetHealth is null || _targetHealth.IsDead)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            _angle += Time.fixedDeltaTime * _angularSpeed;

            var positionOffset = new Vector2(
                Mathf.Cos(_angle) * _chasingDistance,
                Mathf.Sin(_angle) * _chasingDistance);

            var newPosition = (Vector2)_targetHealth.transform.position + positionOffset;
            var direction = (newPosition - _rigidbody2D.position).normalized;
            var move = direction * _enemyData.MoveSpeed * Time.fixedDeltaTime;
            _rigidbody2D.AddForce(move);
            if (_rigidbody2D.velocity.magnitude > _enemyData.MaxMoveSpeed)
                _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity,
                    _enemyData.MaxMoveSpeed * _gameplayManager.GetGameplayDifficulty().EnemyMaxMoveSpeedMultiply);
        }

        _entityAnimator.SetBool("Idle", _rigidbody2D.velocity.magnitude < float.Epsilon);
    }

    private void ShootHandle()
    {
        if (_healthBehaviour.IsDead || _healthBehaviour.IsHitted ||
            _targetHealth is null || _targetHealth.IsDead)
            return;

        _shootSound.Play();
        var direction = ((Vector2)_targetHealth.transform.position - _rigidbody2D.position).normalized;
        Instantiate(_projectilePrefab, transform.position, Quaternion.identity)
            .Init(_targetHealth, _enemyData.Damage, direction);
    }

    #endregion

}
