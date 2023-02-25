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
    protected HealthBehaviour _health;

    [SerializeField]
    protected bool _isHit;

    [SerializeField]
    protected bool _isDead;

    [Header("Animation")]
    [SerializeField]
    protected Animator _entityAnimator;

    [Header("Sound")]
    [SerializeField]
    protected AudioSource _hitSound;

    #endregion

    #region Properties

    public bool IsDead => _isDead;

    #endregion

    #region Methods

    protected virtual void Awake()
    {
        if (_rigidbody2D == null) _rigidbody2D = GetComponent<Rigidbody2D>();
        _isDead = false;
        _isHit = false;
    }

    protected virtual void OnEnable()
    {
        _health.OnHealthChanged += HealthEvent;
    }

    protected virtual void OnDisable()
    {
        _health.OnHealthChanged -= HealthEvent;
    }

    protected virtual void HealthEvent(float health, float maxHealth)
    {
        if (health <= 0)
        {
            _isDead = true;
            GetComponent<CapsuleCollider2D>().enabled = false;
            _rigidbody2D.velocity = Vector2.zero;
        }

        _entityAnimator.SetFloat("Health", health);
        _entityAnimator.SetTrigger("Hit");
    }

    public void OnHitStart()
    {
        _isHit = true;
        _rigidbody2D.velocity = Vector2.zero;
        _hitSound.pitch = Random.Range(0.9f, 1.1f);
        _hitSound.Play();
    }

    public void OnHitEnd()
    {
        _isHit = false;
    }

    #endregion

}
