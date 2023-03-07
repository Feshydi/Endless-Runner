using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BurstType
{
    Disabled,
    BulletPenetrate,
    IncreaseFireRate,
    IncreaseBulletParticles
}

[CreateAssetMenu(menuName = "Data/CharacterStatsBuff")]
public class CharacterStatsBuffData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private bool _isBulletsPenetrate;

    [SerializeField]
    private float _defaultBulletParticleMultiplier;

    [SerializeField]
    private float _defaultFireRateMultiplier;

    [SerializeField]
    private float _buffedBulletParticleMultiplier;

    [SerializeField]
    private float _buffedFireRateMultiplier;

    [SerializeField]
    private float _currentBulletParticleMultiplier;

    [SerializeField]
    private float _currentFireRateMultiplier;

    #endregion

    #region Properties

    public bool IsBulletPenetrate => _isBulletsPenetrate;

    public float BulletParticleMultiplier => _currentBulletParticleMultiplier;

    public float FireRateMultiplier => _currentFireRateMultiplier;

    #endregion

    #region Methods

    private void OnEnable()
    {
        _currentBulletParticleMultiplier = _defaultBulletParticleMultiplier;
        _currentFireRateMultiplier = _defaultFireRateMultiplier;
    }

    private void OnDisable()
    {
        _currentBulletParticleMultiplier = _defaultBulletParticleMultiplier;
        _currentFireRateMultiplier = _defaultFireRateMultiplier;
    }

    public void SetBurstEffect(BurstType burstType)
    {
        switch (burstType)
        {
            case BurstType.BulletPenetrate:
                _isBulletsPenetrate = true;
                break;
            case BurstType.IncreaseBulletParticles:
                _currentBulletParticleMultiplier = _buffedBulletParticleMultiplier;
                break;
            case BurstType.IncreaseFireRate:
                _currentFireRateMultiplier = _buffedFireRateMultiplier;
                break;
            case BurstType.Disabled:
                _isBulletsPenetrate = false;
                _currentBulletParticleMultiplier = _defaultBulletParticleMultiplier;
                _currentFireRateMultiplier = _defaultFireRateMultiplier;
                break;
        }
    }

    #endregion

}
