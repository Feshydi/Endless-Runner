using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    protected WeaponData _weaponData;

    [SerializeField]
    protected CharacterStatsBuffData _statsBuffData;

    [Header("Projectile")]
    [SerializeField]
    protected Projectile _projectilePrefab;

    [SerializeField]
    private SpriteRenderer _muzzleSpriteRenderer;

    [SerializeField]
    protected Transform _muzzlePosition;

    [Header("Sound")]
    [SerializeField]
    protected AudioSource _shootSound;

    [Header("Generated Data")]
    [SerializeField]
    protected float _nextShotTime;

    [SerializeField]
    protected List<Projectile> _spawnedProjectiles;

    #endregion

    #region Methods

    private void OnDestroy()
    {
        foreach (var projectile in _spawnedProjectiles)
        {
            if (projectile != null)
                Destroy(projectile.gameObject);
        }
    }

    protected virtual void Start()
    {
        _spawnedProjectiles = new List<Projectile>();
        _muzzleSpriteRenderer.enabled = false;
    }

    public abstract void Shoot();

    protected bool ReadyToShoot()
    {
        if (Time.time < _nextShotTime)
            return false;
        _nextShotTime = Time.time + 60 / (_weaponData.FireRate * _statsBuffData.FireRateMultiplier);
        return true;
    }

    protected IEnumerator OnFire()
    {
        _muzzleSpriteRenderer.enabled = true;
        _shootSound.pitch = Random.Range(0.9f, 1.1f);
        _shootSound.Play();
        yield return new WaitForSeconds(0.05f);
        _muzzleSpriteRenderer.enabled = false;
    }

    #endregion

}
