using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleProjectile : Projectile
{

    #region Fields

    [Header("Particle Data")]
    [SerializeField]
    private HealthBehaviour _ignoredEnemy;

    #endregion

    #region Methods

    public void SetIgnored(HealthBehaviour enemy)
    {
        _ignoredEnemy = enemy;
    }

    protected override void DoOnCollision(Collider2D collision)
    {
        if (_ignoredEnemy is null || !collision.Equals(_ignoredEnemy))
        {
            DoDamage(collision);
            Destroy(gameObject);
        }

    }

    #endregion

}

