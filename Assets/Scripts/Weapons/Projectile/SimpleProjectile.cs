using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : Projectile
{

    #region Methods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerControllerBehaviour pcb))
            return;

        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.DoDamage(_damage);
        }

        Destroy(gameObject);
    }

    #endregion

}

