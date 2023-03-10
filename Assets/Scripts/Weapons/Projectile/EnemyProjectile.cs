using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private HealthBehaviour _targetHealth;

    [SerializeField]
    protected float _damage;

    [SerializeField]
    protected float _speed = 5f;

    [SerializeField]
    protected Vector2 _direction;

    [SerializeField]
    protected float _flyDistance;

    [SerializeField]
    protected float _maxFlyDistance = 20f;

    #endregion

    #region Methods

    public void Init(HealthBehaviour targetHealth, float damage, Vector2 direction)
    {
        _targetHealth = targetHealth;
        _damage = damage;
        _direction = direction;
    }

    private void FixedUpdate()
    {
        var move = _direction * _speed * Time.fixedDeltaTime;
        _rigidbody2D.MovePosition(_rigidbody2D.position + move);
        _flyDistance += move.magnitude;
        if (_flyDistance > _maxFlyDistance)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            if (damageable.Equals(_targetHealth))
            {
                damageable.DoDamage(_damage);
                Destroy(gameObject);
            }
        }
    }

    #endregion

}
