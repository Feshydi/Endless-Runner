using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : Projectile
{

    #region Methods

    protected override void DoOnCollision(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(_damage);
        }
        Destroy(gameObject);
    }

    #endregion

}

