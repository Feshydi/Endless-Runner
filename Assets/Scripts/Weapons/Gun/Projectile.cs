using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private Vector2 _direction;

    [SerializeField]
    private float _flyDistance;

    [SerializeField]
    private float _maxFlyDistance = 20f;

    #endregion

    #region Methods

    public void Init(WeaponData weaponData, Vector2 direction)
    {
        _damage = weaponData.Damage;
        _speed = weaponData.ProjectileSpeed;
        _direction = direction;
        _flyDistance = 0f;
    }

    private void Update()
    {
        var move = _direction * _speed * Time.deltaTime;
        _flyDistance += (move).magnitude;
        if (_flyDistance > _maxFlyDistance)
            Destroy(gameObject);

        transform.Translate(move);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(_damage);
        }

        Destroy(gameObject);
    }

    #endregion

}
