using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemyController : EnemyController
{

    #region Fields

    [SerializeField]
    private float _minChargeDistance;

    [SerializeField]
    private float _chargeSpeed;

    [SerializeField]
    private float _chargeForce;

    [SerializeField]
    private float _chargeDamage;

    [SerializeField]
    private float _chargeCooldownTime;

    [SerializeField]
    private float _nextChargeTime;

    [SerializeField]
    private bool _isCharging;

    [SerializeField]
    private AudioSource _attackSound;

    [SerializeField]
    private AudioSource _chargeSound;

    #endregion

    #region Methods

    private void FixedUpdate()
    {
        if (!_isCharging)
            MoveHandle();

        var distance = Vector2.Distance(transform.position, _targetHealth.transform.position);
        if (Time.time > _nextChargeTime && distance >= _minChargeDistance)
        {
            _nextChargeTime = Time.time + _chargeCooldownTime;
            StartCoroutine(ChargeHandle());
        }
    }

    private IEnumerator ChargeHandle()
    {
        if (_healthBehaviour.IsDead || _healthBehaviour.IsHitted)
            yield return null;

        _chargeSound.Play();

        _isCharging = true;
        _entityAnimator.SetBool("isCharging", true);
        var direction = (_targetHealth.transform.position - transform.position).normalized;
        var move = direction * _chargeSpeed;
        _rigidbody2D.AddForce(move, ForceMode2D.Impulse);
        yield return new WaitUntil(() => _rigidbody2D.velocity.magnitude > _enemyData.MaxMoveSpeed);
        _isCharging = false;
        _entityAnimator.SetBool("isCharging", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isCharging)
            StartCoroutine(TryPush(collision));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_isCharging)
            StartCoroutine(TryPush(collision));

        if (Time.time < _nextHitTime)
            return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            if (damageable.Equals(_targetHealth))
            {
                _attackSound.Play();

                damageable.DoDamage(_enemyData.Damage);
                _nextHitTime = Time.time + 60 / (_enemyData.DamageRate * _gameplayManager.GetGameplayDifficulty().EnemyDamageMultiply);
            }
        }
    }

    private IEnumerator TryPush(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthBehaviour healthBehaviour))
        {
            Vector2 direction = (collision.transform.position - transform.position).normalized;
            Vector2 move;
            if (healthBehaviour.Equals(_healthBehaviour))
                move = direction * _chargeForce;
            else
            {
                healthBehaviour.SetIsHitted(true);
                move = Vector2.Perpendicular(direction) * _chargeForce;
            }
            collision.rigidbody.AddForce(move, ForceMode2D.Impulse);

            yield return new WaitUntil(() => collision.rigidbody.velocity.magnitude >
                _enemyData.MaxMoveSpeed * _gameplayManager.GetGameplayDifficulty().EnemyMaxMoveSpeedMultiply);

            if (healthBehaviour.Equals(_healthBehaviour))
            {
                healthBehaviour.DoDamage(_chargeDamage);
                _healthBehaviour.DoDamage(_chargeDamage);
            }
            else
                healthBehaviour.SetIsHitted(false);
        }
    }

    #endregion

}
