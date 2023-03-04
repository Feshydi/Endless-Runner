using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    protected bool _isKnockbacking;

    [SerializeField]
    protected float _knockbackingStrength;

    protected IKnockbackable _knockbackable;

    [SerializeField]
    private bool _isCollided;

    [Header("Initiated Data")]
    [SerializeField]
    protected float _damage;

    [SerializeField]
    protected float _speed;

    [SerializeField]
    protected Vector2 _direction;

    [SerializeField]
    protected float _flyDistance;

    [SerializeField]
    protected float _maxFlyDistance = 20f;

    #endregion

    #region Methods

    private void DataCheck()
    {
        _knockbackable = _isKnockbacking ? new KnockbackingBehaviour() : new DisabledKnockbackingBehaviour();

        if (_rigidbody2D is null) _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, float speed, Vector2 direction)
    {
        DataCheck();
        _damage = damage;
        _speed = speed;
        _direction = direction;
    }

    public void Init(float damage, float speed, Vector2 direction, float maxFlyDistance)
    {
        Init(damage, speed, direction);
        _maxFlyDistance = maxFlyDistance;
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
        if (collision.TryGetComponent(out PlayerControllerBehaviour pcb)
            || collision.TryGetComponent(out Projectile p))
            return;

        if (_isCollided)
            return;
        _isCollided = true;

        DoOnCollision(collision);
    }

    protected abstract void DoOnCollision(Collider2D collision);

    protected void DoDamage(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(_damage);
            if (collision.TryGetComponent(out Rigidbody2D rb))
                _knockbackable.Knockback(rb, _direction, _knockbackingStrength);
        }
    }

    #endregion

}
