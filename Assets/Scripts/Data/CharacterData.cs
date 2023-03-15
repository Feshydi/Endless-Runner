using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Character")]
public class CharacterData : EntityData
{

    #region Fields

    [SerializeField]
    [TextArea(3, 5)]
    private string _explosionDescription;

    [SerializeField]
    private float _explosionRadius;

    [SerializeField]
    private float _explosionForce;

    [SerializeField]
    private float _explosionDamage;

    [SerializeField]
    private float _explosionCooldownTime;

    [SerializeField]
    [TextArea(3, 5)]
    private string _rollDescription;

    [SerializeField]
    private float _rollCooldownTime;

    [SerializeField]
    [TextArea(3, 5)]
    private string _burstDescription;

    [SerializeField]
    private float _burstTime;

    [SerializeField]
    private float _burstCooldownTime;

    #endregion

    #region Properties

    public string ExplosionDescription => _explosionDescription;

    public float ExplosionRadius => _explosionRadius;

    public float ExplosionForce => _explosionForce;

    public float ExplosionDamage => _explosionDamage;

    public float ExplosionCooldownTime => _explosionCooldownTime;

    public string RollDescription => _rollDescription;

    public float RollCooldownTime => _rollCooldownTime;

    public string BurstDescription => _burstDescription;

    public float BurstTime => _burstTime;

    public float BurstCooldownTime => _burstCooldownTime;

    #endregion

}
