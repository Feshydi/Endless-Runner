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

    public event Action<float, float> OnHealthChanged;

    #endregion

    #region Properties

    public float HealthPoints => _healthPoints;

    public bool IsDamageAllowed
    {
        get => _isDamageAllowed;
        set => _isDamageAllowed = value;
    }

    public bool IsDead
    {
        get => _isDead;
        set => _isDead = value;
    }

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
        if (_healthPoints < 0)
        {
            _healthPoints = 0;
            _isDead = true;
        }

        OnHealthChanged?.Invoke(_healthPoints, _entityData.HealthPoints);
    }

    #endregion

}
