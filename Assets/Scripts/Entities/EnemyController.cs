using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EntityController
{

    #region Fields

    [Header("Enemy Data")]
    [SerializeField]
    private EnemyData _enemyData;

    [SerializeField]
    private PlayerController _target;

    [Header("Generated Data")]
    [SerializeField]
    private Health _targetHealth;

    [SerializeField]
    private CapsuleCollider2D _targetCollider;

    [SerializeField]
    private float _nextHitTime;

    #endregion

    #region Methods

    public void Init(PlayerController target)
    {
        _entityAnimator.SetFloat("Health", _enemyData.HealthPoints);
        _target = target;
        _targetHealth = target.GetComponent<Health>();
        _targetCollider = target.GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        MoveHandle();
    }

    private void MoveHandle()
    {
        if (_isDead || _isHit)
            return;

        if (_target == null || _target.IsDead)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            var playerPos = _target.transform.position;
            var distance = Vector2.Distance(transform.position, playerPos);
            if (distance > _targetCollider.bounds.extents.magnitude)
            {
                Vector2 direction = (playerPos - transform.position).normalized;
                _rigidbody2D.velocity = direction * _enemyData.MoveSpeed * Time.fixedDeltaTime;
            }
        }

        if (_rigidbody2D.velocity.sqrMagnitude > 0.01)
            _entityAnimator.SetBool("Idle", false);
        else
            _entityAnimator.SetBool("Idle", true);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (Time.time < _nextHitTime)
            return;

        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            if (damageable.Equals(_targetHealth) && other.gameObject.GetComponent<Health>().enabled)
            {
                damageable.DoDamage(_enemyData.Damage);
                _nextHitTime = Time.time + 60 / _enemyData.DamageRate;
            }
        }
    }

    private void AfterDeath()
    {
        GameManager.Instance.ScoreManager.AddScore(1);
        Destroy(gameObject);
    }

    #endregion
}