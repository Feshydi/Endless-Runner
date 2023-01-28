using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class EnemyController : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private EnemyData _enemyData;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private PlayerController _playerController;

    [Header("Generated Data")]
    [SerializeField]
    private bool _isLookingLeft;

    [SerializeField]
    private bool _isDead;

    [Header("Animation Settings")]
    [SerializeField]
    private SpriteRenderer _enemySpriteRenderer;

    [SerializeField]
    private Animator _enemyAnimator;

    [Header("Additional")]
    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void OnEnable()
    {
        GetComponent<Health>().OnHealthChanged += HealthEvent;
    }

    private void OnDisable()
    {
        GetComponent<Health>().OnHealthChanged -= HealthEvent;
    }

    public void Init(PlayerController playerController)
    {
        _enemyAnimator.SetFloat("Health", _enemyData.HealthPoints);
        _playerController = playerController;
    }

    private void Update()
    {
        if (_playerController == null || _isDead)
            return;

        FlipByDirection();
    }

    private void FixedUpdate()
    {
        if (_playerController == null || _isDead)
            return;

        var playerPos = _playerController.transform.position;

        float distance = Vector2.Distance(transform.position, playerPos);
        if (distance > float.Epsilon)
        {
            Vector2 direction = (playerPos - transform.position).normalized;
            _rigidbody2D.velocity = direction * _enemyData.MoveSpeed;
        }
    }

    private void FlipByDirection()
    {
        if (_playerController.transform.position.x < transform.position.x)
        {
            if (!_isLookingLeft)
            {
                FlipLeft(true);
                _logger.Log($"{gameObject} looking at the right", this);
            }
        }
        else
        {
            if (_isLookingLeft)
            {
                FlipLeft(false);
                _logger.Log($"{gameObject} looking at the left", this);
            }
        }
    }

    private void FlipLeft(bool value)
    {
        _isLookingLeft = value;
        _enemySpriteRenderer.flipX = value;
    }

    private void HealthEvent(float health)
    {
        _enemyAnimator.SetFloat("Health", health);
    }

    #endregion
}