using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private CharacterData _characterData;

    [SerializeField]
    private PlayerControls _inputActions;

    [SerializeField]
    private Camera _camera;

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

    private void Awake()
    {
        _spawnedProjectiles = new List<Projectile>();

        _muzzleSpriteRenderer.enabled = false;
    }

    public void Shoot()
    {
        if (Time.time < _nextShotTime)
            return;
        _nextShotTime = Time.time + 60 / _characterData.DamageRate;

        Vector2 shootDirection = transform.right;
        StartCoroutine(OnFire());
        var projectile = Instantiate(_projectilePrefab, _muzzlePosition.position, _muzzlePosition.localRotation);
        projectile.Init(_characterData, shootDirection);
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
