using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
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
    private float _nextShotTime;

    #endregion

    #region Methods

    public void Init(PlayerController target)
    {
        _entityAnimator.SetFloat("Health", _enemyData.HealthPoints);
        _target = target;
    }

    private void FixedUpdate()
    {
        if (_isDead || _target == null || _target.IsDead)
            return;

        var playerPos = _target.transform.position;

        float distance = Vector2.Distance(transform.position, playerPos);
        if (distance > float.Epsilon)
        {
            Vector2 direction = (playerPos - transform.position).normalized;
            _rigidbody2D.velocity = direction * _enemyData.MoveSpeed;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (Time.time < _nextShotTime)
            return;

        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(_enemyData.Damage);
            _nextShotTime = Time.time + 60 / _enemyData.DamageRate;
        }
    }

    #endregion
}