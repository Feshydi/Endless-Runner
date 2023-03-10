using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyController : EnemyController
{

    #region Methods

    private void FixedUpdate()
    {
        MoveHandle();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (Time.time < _nextHitTime)
            return;

        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            if (damageable.Equals(_targetHealth))
            {
                damageable.DoDamage(_enemyData.Damage);
                _nextHitTime = Time.time + 60 / _enemyData.DamageRate;
            }
        }
    }

    #endregion

}
