using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{

    #region Fields

    [Header("Initiated Data")]
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private bool _isCollided;

    [SerializeField]
    protected float _damage;

    [SerializeField]
    protected float _speed;

    [SerializeField]
    protected Vector2 _direction;

    [SerializeField]
    protected float _flyDistance = 0f;

    [SerializeField]
    protected float _maxFlyDistance = 20f;

    #endregion

    #region Methods

    public void Init(float damage, float speed, Vector2 direction)
    {
        if (_rigidbody2D is null) _rigidbody2D = GetComponent<Rigidbody2D>();

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
        if (_isCollided)
            return;
        _isCollided = true;

        if (collision.gameObject.TryGetComponent(out PlayerControllerBehaviour pcb))
            return;

        DoOnCollision(collision);
    }

    protected abstract void DoOnCollision(Collider2D collision);

    #endregion

}
