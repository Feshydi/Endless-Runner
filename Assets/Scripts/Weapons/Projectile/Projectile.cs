using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    #region Fields

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
    protected float _maxFlyDistance;

    #endregion

    #region Methods

    public void Init(float damage, float speed, Vector2 direction)
    {
        _damage = damage;
        _speed = speed;
        _direction = direction;
        _flyDistance = 0F;
        _maxFlyDistance = 20f;
    }

    private void Update()
    {
        var move = _direction * _speed * Time.deltaTime;
        _flyDistance += move.magnitude;
        if (_flyDistance > _maxFlyDistance)
            Destroy(gameObject);

        transform.Translate(move);
    }

    #endregion

}
