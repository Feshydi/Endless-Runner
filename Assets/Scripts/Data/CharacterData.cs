using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character")]
public class CharacterData : EntityData
{

    #region Fields

    [SerializeField]
    private float _explosionRadius;

    [SerializeField]
    private float _explosionForce;

    [SerializeField]
    private float _explosionDamage;

    [SerializeField]
    private float _explosionCooldownTime;

    [SerializeField]
    private float _rollCooldownTime;

    [SerializeField]
    private float _burstTime;

    [SerializeField]
    private float _burstCooldownTime;

    #endregion

    #region Properties

    public float ExplosionRadius => _explosionRadius;

    public float ExplosionForce => _explosionForce;

    public float ExplosionDamage => _explosionDamage;

    public float ExplosionCooldownTime => _explosionCooldownTime;

    public float RollCooldownTime => _rollCooldownTime;

    public float BurstTime => _burstTime;

    public float BurstCooldownTime => _burstCooldownTime;

    #endregion

}
