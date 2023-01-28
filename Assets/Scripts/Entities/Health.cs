using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private EntityData _entityData;

    [SerializeField]
    private float _healthPoints;

    [SerializeField]
    public Action<float> OnHealthChanged;

    [Header("Additional")]
    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void Awake()
    {
        _healthPoints = _entityData.HealthPoints;
        _logger.Log($"{gameObject}'s health: {_healthPoints}", this);
    }

    public void DoDamage(float damage)
    {
        _healthPoints -= damage;
        OnHealthChanged?.Invoke(_healthPoints);

        _logger.Log($"{gameObject}'s new health: {_healthPoints}", this);
    }

    #endregion

}
