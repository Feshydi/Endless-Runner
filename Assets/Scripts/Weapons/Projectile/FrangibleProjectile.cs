using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrangibleProjectile : Projectile
{

    #region Fields

    [Header("Frangible Data")]
    [SerializeField]
    private int _particlesCount;

    [SerializeField]
    private ParticleProjectile _particlePrefab;

    [SerializeField]
    private float _particleDamageMultiply;

    [SerializeField]
    private float _particleSpeedMultiply;

    #endregion

    #region Methods

    private void OnDestroy()
    {
        CreateParticles(null);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerControllerBehaviour pcb))
            return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(_damage);
        }
        CreateParticles(collision);

        Destroy(gameObject);
    }

    private void CreateParticles(Collision2D collision)
    {
        float angle = 360 / _particlesCount;
        for (int i = 1; i <= _particlesCount; i++)
        {
            Vector2 direction = Quaternion.Euler(0, 0, angle * i) * Vector2.right;
            var particleProjectile = Instantiate(_particlePrefab, transform.position, transform.rotation);
            particleProjectile.Init(_damage * _particleDamageMultiply, _speed * _particleSpeedMultiply, direction);
            if (collision is not null)
                if (collision.gameObject.TryGetComponent(out HealthBehaviour health))
                    particleProjectile.SetIgnored(health);
        }
    }

    #endregion

}

