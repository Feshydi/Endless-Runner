using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class EntityController : MonoBehaviour
{

    #region Fieldsw

    [Header("Entity Data")]
    [SerializeField]
    protected Rigidbody2D _rigidbody2D;

    [SerializeField]
    protected Health _health;

    [SerializeField]
    protected bool _isDead;

    [Header("Animation")]
    [SerializeField]
    protected Animator _entityAnimator;

    [Header("Additional")]
    [SerializeField]
    protected Logger _logger;

    #endregion

    #region Methods

    protected virtual void Awake()
    {
        if (_rigidbody2D == null) _rigidbody2D = GetComponent<Rigidbody2D>();
        _isDead = false;
    }

    protected virtual void OnEnable()
    {
        _health.OnHealthChanged += HealthEvent;
    }

    protected virtual void OnDisable()
    {
        _health.OnHealthChanged -= HealthEvent;
    }

    protected virtual void HealthEvent(float health)
    {
        if (health <= 0)
        {
            _isDead = true;
            _rigidbody2D.velocity = Vector2.zero;
        }

        _entityAnimator.SetFloat("Health", health);
        _entityAnimator.SetTrigger("Hit");
    }

    protected virtual void AfterDeath()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
    }

    #endregion

}
