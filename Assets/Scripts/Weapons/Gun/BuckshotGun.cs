using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckshotGun : Gun
{

    #region #fields

    [Header("Shotgun Data")]
    [SerializeField]
    private int _projectilesCount;

    [SerializeField]
    private float _projectileSpread;

    #endregion

    #region Methods

    public override void Shoot()
    {
        if (!ReadyToShoot())
            return;

        StartCoroutine(OnFire());

        for (int i = 0; i < _projectilesCount; i++)
        {
            Vector2 shootDirection = Quaternion.Euler(0, 0, Random.Range(-_projectileSpread, _projectileSpread)) * transform.right;
            var projectile = Instantiate(_projectilePrefab, _muzzlePosition.position, _muzzlePosition.rotation);
            projectile.Init(_weaponData.Damage, _weaponData.ProjectileSpeed, shootDirection);
            _spawnedProjectiles.Add(projectile);
        }
    }

    #endregion

}
