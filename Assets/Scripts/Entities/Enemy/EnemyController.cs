using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{

    #region Fields

    [Header("Enemy Data")]
    [SerializeField]
    protected EnemyData _enemyData;

    [SerializeField]
    protected Rigidbody2D _rigidbody2D;

    [SerializeField]
    protected HealthBehaviour _healthBehaviour;

    [SerializeField]
    protected float _chasingDistance;

    [Header("Target Data")]
    [SerializeField]
    protected HealthBehaviour _targetHealth;

    [Header("Generated Data")]
    [SerializeField]
    protected float _nextHitTime;

    [Header("Animation")]
    [SerializeField]
    protected Animator _entityAnimator;

    [Header("Sound")]
    [SerializeField]
    protected AudioSource _hitSound;

    #endregion

    #region Methods

    public void Init(PlayerControllerBehaviour target)
    {
        _entityAnimator.SetFloat("Health", _enemyData.HealthPoints);
        _targetHealth = target.GetComponent<HealthBehaviour>();
    }

    protected virtual void Start()
    {
        CheckVariables();
    }

    private void OnEnable()
    {
        _healthBehaviour.OnHealthValueEvent += HealthEvent;
    }

    private void OnDisable()
    {
        _healthBehaviour.OnHealthValueEvent -= HealthEvent;
    }

    protected void MoveHandle()
    {
        if (_healthBehaviour.IsDead || _healthBehaviour.IsHitted)
            return;

        if (_targetHealth is null || _targetHealth.IsDead)
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            Vector2 playerPos = _targetHealth.transform.position;
            var distance = Vector2.Distance(transform.position, _targetHealth.transform.position);
            if (distance >= _chasingDistance)
            {
                var direction = (playerPos - (Vector2)transform.position).normalized;
                var move = direction * _enemyData.MoveSpeed * Time.fixedDeltaTime;
                _rigidbody2D.AddForce(move);
                if (_rigidbody2D.velocity.magnitude > _enemyData.MaxMoveSpeed)
                    _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, _enemyData.MaxMoveSpeed);
            }
            else
            {
                var offset = _rigidbody2D.position - playerPos;
                _rigidbody2D.position = Vector2.ClampMagnitude(offset, _chasingDistance);
            }
        }

        _entityAnimator.SetBool("Idle", _rigidbody2D.velocity.magnitude < float.Epsilon);
    }

    protected virtual void CheckVariables()
    {
        if (_rigidbody2D is null) _rigidbody2D = GetComponent<Rigidbody2D>();
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

    public void OnHitEnd()
    {
        _healthBehaviour.SetIsHitted(false);
    }

    public void AfterDeath()
    {
        Destroy(gameObject);
    }

    #endregion

}
