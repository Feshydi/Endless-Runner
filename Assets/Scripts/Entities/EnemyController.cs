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

    //[SerializeField]
    //private bool _isLookingLeft;

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

    //private void Update()
    //{
    //    if (_playerController == null || _isDead)
    //        return;

    //       FlipByDirection();
    //}

    private void FixedUpdate()
    {
        if (_target == null || _isDead)
            return;

        var playerPos = _target.transform.position;

        float distance = Vector2.Distance(transform.position, playerPos);
        if (distance > float.Epsilon)
        {
            Vector2 direction = (playerPos - transform.position).normalized;
            _rigidbody2D.velocity = direction * _enemyData.MoveSpeed;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time < _nextShotTime)
            return;
        _nextShotTime = Time.time + 60 / _enemyData.DamageRate;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(_enemyData.Damage);
        }
    }

    //private void FlipByDirection()
    //{
    //    if (_playerController.transform.position.x < transform.position.x)
    //    {
    //        if (!_isLookingLeft)
    //        {
    //            FlipLeft(true);
    //            _logger.Log($"{gameObject} looking at the right", this);
    //        }
    //    }
    //    else
    //    {
    //        if (_isLookingLeft)
    //        {
    //            FlipLeft(false);
    //            _logger.Log($"{gameObject} looking at the left", this);
    //        }
    //    }
    //}

    //private void FlipLeft(bool value)
    //{
    //    _isLookingLeft = value;
    //    _enemySpriteRenderer.flipX = value;
    //}

    #endregion
}