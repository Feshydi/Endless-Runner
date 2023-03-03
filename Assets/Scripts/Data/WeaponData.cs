using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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

    [SerializeField]
    private Vector2 _weaponPosition;

    [SerializeField]
    private Vector2 _muzzlePosition;

    [SerializeField]
    private Vector2 _muzzleScale;

    [SerializeField]
    private AnimatorController _animatorController;

    #endregion

    #region Properties

    public float Damage => _damage;

    public float FireRate => _fireRate;

    public float ProjectileSpeed => _projectileSpeed;

    public Vector2 WeaponPosition => _weaponPosition;

    public Vector2 MuzzlePosition => _muzzlePosition;

    public Vector2 MuzzleScale => _muzzleScale;

    public AnimatorController AnimatorController => _animatorController;

    #endregion

}
