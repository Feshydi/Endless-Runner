using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : Projectile
{

    #region Methods

    protected override void DoOnCollision(Collider2D collision)
    {
        DoDamage(collision);
        if (!_statsBuffData.IsBulletPenetrate)
            Destroy(gameObject);
    }

    #endregion

}
