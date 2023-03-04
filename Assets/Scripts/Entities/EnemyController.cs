using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    #region Fields

    [Header("Enemy Data")]
    [SerializeField]
    private EnemyData _enemyData;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private HealthBehaviour _healthBehaviour;

    [Header("Target Data")]
    [SerializeField]
    private HealthBehaviour _targetHealth;

    [Header("Generated Data")]
    [SerializeField]
    private float _nextHitTime;

    [Header("Animation")]
    [SerializeField]
    private Animator _entityAnimator;

    [Header("Sound")]
    [SerializeField]
    private AudioSource _hitSound;

    #endregion

    #region Methods

    public void Init(PlayerControllerBehaviour target)
    {
        _entityAnimator.SetFloat("Health", _enemyData.HealthPoints);
        _targetHealth = target.GetComponent<HealthBehaviour>();
    }

    private void Start()
    {
        if (_rigidbody2D is null) _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _healthBehaviour.OnHealthValueEvent += HealthEvent;
    }

    private void OnDisable()
    {
        _healthBehaviour.OnHealthValueEvent -= HealthEvent;
    }

    private void FixedUpdate()
    {
        MoveHandle();
    }

    private void MoveHandle()
    {
        if (_healthBehaviour.IsDead || _healthBehaviour.IsHitted)
            return;

        if (_targetHealth is null || _targetHealth.IsDead)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            var playerPos = _targetHealth.transform.position;
            var distance = Vector2.Distance(transform.position, _targetHealth.transform.position);
            if (distance > 0)
            {
                Vector2 direction = (playerPos - transform.position).normalized;
                var move = direction * _enemyData.MoveSpeed * Time.fixedDeltaTime;
                _rigidbody2D.AddForce(move);
                if (_rigidbody2D.velocity.magnitude > _enemyData.MaxMoveSpeed)
                    _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, _enemyData.MaxMoveSpeed);
            }
        }

        _entityAnimator.SetBool("Idle", _rigidbody2D.velocity.magnitude < float.Epsilon);
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

    private void HealthEvent(float health, float maxHealth)
    {
        if (health <= 0)
        {
            _healthBehaviour.SetIsDead(true);
            GetComponent<CapsuleCollider2D>().enabled = false;
            _rigidbody2D.velocity = Vector2.zero;

            GameManager.Instance?.ScoreManager.AddScore(1);
        }

        _entityAnimator.SetFloat("Health", health);
        _entityAnimator.SetTrigger("Hit");
    }

    public void OnHitStart()
    {
        _healthBehaviour.SetIsHitted(true);

        _hitSound.pitch = Random.Range(0.9f, 1.1f);
        _hitSound.Play();
    }

    private void OnHitEnd()
    {
        _healthBehaviour.SetIsHitted(false);
    }

    private void AfterDeath()
    {
        Destroy(gameObject);
    }

    #endregion

}
