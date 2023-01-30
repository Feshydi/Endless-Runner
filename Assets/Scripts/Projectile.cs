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

    #endregion

    #region Methods

    public void Init(CharacterData characterData, Vector2 direction)
    {
        _damage = characterData.Damage;
        _speed = characterData.ProjectileSpeed;
        _direction = direction;
        _flyDistance = 0f;
    }

    private void Update()
    {
        var move = _direction * _speed * Time.deltaTime;
        _flyDistance += (move).magnitude;
        if (_flyDistance > 20)
            Destroy(gameObject);

        transform.Translate(move);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(_damage);
        }

        Destroy(gameObject);
    }

    #endregion

}
