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
    private float _healthPoints;

    [SerializeField]
    private bool _isDamageAllowed = true;

    [SerializeField]
    private bool _isDead;

    [SerializeField]
    private bool _isHitted;

    public event Action<float, float> OnHealthValueEvent;

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
    }

    public void DoDamage(float damage)
    {
        if (!_isDamageAllowed)
            return;

        _healthPoints -= damage;
        _isHitted = true;
        _isDamageAllowed = false;
        if (_healthPoints < 0)
        {
            _healthPoints = 0;
            _isDead = true;
        }

        OnHealthValueEvent?.Invoke(_healthPoints, _entityData.HealthPoints);
    }

    public void AfterHit()
    {
        _isHitted = false;
        _isDamageAllowed = true;
    }

    public void SetIsDead(bool value)
    {
        _isDead = value;
    }

    public void SetIsHitted(bool value)
    {
        _isHitted = value;
        _isDamageAllowed = !value;
    }

    #endregion

}
