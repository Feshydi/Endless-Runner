﻿using System.Collections;
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

    [SerializeField]
    private float _particleFlyDistance;

    #endregion

    #region Methods

    private void OnDestroy()
    {
        CreateParticles(null);
    }

    protected override void DoOnCollision(Collider2D collision)
    {
        DoDamage(collision);
        CreateParticles(collision);
        if (!_statsBuffData.IsBulletPenetrate)
            Destroy(gameObject);
    }

    private void CreateParticles(Collider2D collision)
    {
        var particlesCount = (int)(_particlesCount * _statsBuffData.BulletParticleMultiplier);
        float angle = 360 / particlesCount;
        for (int i = 1; i <= particlesCount; i++)
        {
            Vector2 direction = Quaternion.Euler(0, 0, angle * i) * Vector2.right;
            var particleProjectile = Instantiate(_particlePrefab, transform.position, transform.rotation);
            particleProjectile.Init(_damage * _particleDamageMultiply, _speed * _particleSpeedMultiply,
                direction, _statsBuffData, _particleFlyDistance);

            if (collision is not null)
                if (collision.gameObject.TryGetComponent(out HealthBehaviour health))
                    particleProjectile.SetIgnored(health);
        }
    }

    #endregion

}
