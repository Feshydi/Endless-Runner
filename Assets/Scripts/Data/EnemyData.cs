using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : EntityData
{

    #region Fields

    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _damageRate;

    #endregion

    #region Properties

    public float Damage => _damage;

    public float DamageRate => _damageRate;

    #endregion

}
