using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _healthPoints;

    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _damageRate;

    #endregion

    #region Properties

    public float MoveSpeed => _moveSpeed;

    public float HealthPoints => _healthPoints;

    public float Damage => _damage;

    public float DamageRate => _damageRate;

    #endregion

}
