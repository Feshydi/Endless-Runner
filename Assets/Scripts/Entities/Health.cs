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

    #endregion

    #region Methods

    private void Start()
    {
        _healthPoints = _entityData.HealthPoints;
    }

    public void DoDamage(float damage)
    {
        _healthPoints -= damage;
        OnHealthChanged?.Invoke(_healthPoints);
    }

    #endregion

}
