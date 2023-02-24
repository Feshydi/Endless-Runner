using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAbilityBehaviour : AbilityBehaviour
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private CharacterData _characterData;

    [Header("Generated")]
    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _radius;

    [SerializeField]
    private float _explosionForce;
    #endregion

    #region Methods

    private void Start()
    {
        _damage = _characterData.ExplosionDamage;
        _radius = _characterData.ExplosionRadius;
        _explosionForce = _characterData.ExplosionForce;
    }

    public override void AbilityHandle()
    {
        var explosionPos = (Vector2)transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, _radius);
        foreach (var hit in colliders)
        {
            StartCoroutine(ExplosionHandle(hit));
        }
    }

    private IEnumerator ExplosionHandle(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rigidbody2D rigidbody2D)
            && collider.TryGetComponent(out EnemyController entity)
            && collider.TryGetComponent(out IDamageable damageable))
        {
            entity.OnHitStart();
            var direction = (collider.transform.position - transform.position).normalized;
            rigidbody2D.AddForce(direction * _explosionForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);
            damageable.DoDamage(_damage);
        }
    }

    #endregion

}

