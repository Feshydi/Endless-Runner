using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player")]
public class CharacterData : EntityData
{

    #region Fields

    [SerializeField]
    private float _projectileSpeed;

    [SerializeField]
    private float _explosionRadius;

    [SerializeField]
    private float _explosionForce;

    [SerializeField]
    private float _explosionDamage;

    #endregion

    #region Properties

    public float ProjectileSpeed => _projectileSpeed;

    public float ExplosionRadius => _explosionRadius;

    public float ExplosionForce => _explosionForce;

    public float ExplosionDamage => _explosionDamage;

    #endregion

}
