using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private WeaponData _weaponData;

    [SerializeField]
    private PlayerControls _inputActions;

    [Header("Projectile")]
    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private SpriteRenderer _muzzleSpriteRenderer;

    [SerializeField]
    private Transform _muzzlePosition;

    [Header("Sound")]
    [SerializeField]
    private AudioSource _shootSound;

    [Header("Generated Data")]
    [SerializeField]
    private float _nextShotTime;

    [SerializeField]
    private List<Projectile> _spawnedProjectiles;

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

    private void Start()
    {
        _spawnedProjectiles = new List<Projectile>();
        _muzzleSpriteRenderer.enabled = false;
    }

    public void Init(WeaponData weaponData)
    {
        _weaponData = weaponData;
        transform.localPosition = _weaponData.WeaponPosition;
        _muzzlePosition.localPosition = _weaponData.MuzzlePosition;
        _muzzlePosition.localScale = _weaponData.MuzzleScale;
    }

    public void Shoot()
    {
        if (Time.time < _nextShotTime)
            return;
        _nextShotTime = Time.time + 60 / _weaponData.FireRate;

        Vector2 shootDirection = transform.right;
        StartCoroutine(OnFire());
        var projectile = Instantiate(_projectilePrefab, _muzzlePosition.position, _muzzlePosition.localRotation);
        projectile.Init(_weaponData, shootDirection);
        _spawnedProjectiles.Add(projectile);
    }

    private IEnumerator OnFire()
    {
        _muzzleSpriteRenderer.enabled = true;
        _shootSound.pitch = Random.Range(0.9f, 1.1f);
        _shootSound.Play();
        yield return new WaitForSeconds(0.05f);
        _muzzleSpriteRenderer.enabled = false;
    }

    #endregion

}
