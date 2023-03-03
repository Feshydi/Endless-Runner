using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugGun : Gun
{

    #region Methods

    public override void Shoot()
    {
        if (!ReadyToShoot())
            return;

        StartCoroutine(OnFire());

        Vector2 shootDirection = transform.right;
        var projectile = Instantiate(_projectilePrefab, _muzzlePosition.position, _muzzlePosition.localRotation);
        projectile.Init(_weaponData.Damage, _weaponData.ProjectileSpeed, shootDirection);
        _spawnedProjectiles.Add(projectile);
    }

    #endregion

}
