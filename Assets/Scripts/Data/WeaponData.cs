using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon")]
public class WeaponData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private float _damage;

    [SerializeField]
    private float _fireRate;

    [SerializeField]
    private float _projectileSpeed;

    #endregion

    #region Properties

    public float Damage => _damage;

    public float FireRate => _fireRate;

    public float ProjectileSpeed => _projectileSpeed;

    #endregion

}
