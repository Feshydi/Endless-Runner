using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour, IDamageable
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private EntityData _entityData;

    [SerializeField]
    private GameplayManager _gameplayManager;

    [SerializeField]
    private float _healthPoints;

    [SerializeField]
    private bool _isDamageAllowed = true;

    [SerializeField]
    private bool _isDead;

    [SerializeField]
    private bool _isHitted;

    public event Action<float, float> OnHealthValueEvent;

    [SerializeField]
    private List<SpriteRenderer> _spritesToBlink;

    #endregion

    #region Properties

    public float HealthPoints => _healthPoints;

    public bool IsDamageAllowed
    {
        get => _isDamageAllowed;
        set => _isDamageAllowed = value;
    }

    public bool IsDead => _isDead;

    public bool IsHitted => _isHitted;

    #endregion

    #region Methods

    private void Start()
    {
        _healthPoints = _entityData.HealthPoints;
        if (TryGetComponent(out EnemyController ec))
            _healthPoints *= _gameplayManager.GetGameplayDifficulty().EnemyHealthMultiply;
    }

    public void DoDamage(float damage)
    {
        if (_isDead || !_isDamageAllowed)
            return;

        _isHitted = true;
        if (TryGetComponent(out EnemyController ec))
            damage *= _gameplayManager.GetGameplayDifficulty().EnemyDamageMultiply;
        _healthPoints -= damage;

        if (TryGetComponent(out PlayerControllerBehaviour pcb))
            _isDamageAllowed = false;

        if (_healthPoints <= 0)
        {
            _healthPoints = 0;
            _isDead = true;
        }

        OnHealthValueEvent?.Invoke(_healthPoints, _entityData.HealthPoints);
    }

    public void AfterHit()
    {
        _isHitted = false;
        if (!_isDead)
            StartCoroutine(BlinkingAnimation());
    }

    private IEnumerator BlinkingAnimation()
    {
        var invincibilityTime = 2.4f;
        var blinkInterval = invincibilityTime / 6;
        var invincibilityTimer = 0.0f;
        while (invincibilityTimer < invincibilityTime)
        {
            foreach (var sprite in _spritesToBlink)
            {
                sprite.enabled = !sprite.enabled;
            }
            invincibilityTimer += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        _isDamageAllowed = true;
    }

    public void SetIsDead(bool value)
    {
        _isDead = value;
    }

    public void SetIsHitted(bool value)
    {
        _isHitted = value;
        _isDamageAllowed = true;
    }

    #endregion

}
