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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerControllerBehaviour pcb))
            return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            if (_ignoredEnemy is null || !damageable.Equals(_ignoredEnemy))
            {
                damageable.DoDamage(_damage);
                Destroy(gameObject);
            }
        }
    }

    #endregion

}

