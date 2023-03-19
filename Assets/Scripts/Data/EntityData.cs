using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _maxMoveSpeed;

    [SerializeField]
    private float _healthPoints;

    #endregion

    #region Properties

    public float MoveSpeed => _moveSpeed;

    public float MaxMoveSpeed => _maxMoveSpeed;

    public float HealthPoints => _healthPoints;

    #endregion

}
