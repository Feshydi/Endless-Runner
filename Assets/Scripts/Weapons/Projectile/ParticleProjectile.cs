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
        if (_ignoredEnemy is not null)
        {
            if (collision.TryGetComponent(out HealthBehaviour health))
                if (health.Equals(_ignoredEnemy))
                    return;
        }

        DoDamage(collision);
        if (!_statsBuffData.IsBulletPenetrate)
            Destroy(gameObject);
    }

    #endregion

}
