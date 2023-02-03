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

    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private SpriteRenderer _muzzleSpriteRenderer;

    [SerializeField]
    private Transform _muzzlePosition;

    [Header("Generated Data")]
    [SerializeField]
    private float _nextShotTime;

    #endregion

    #region Methods

    private void Awake()
    {
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
    }

    private IEnumerator OnFire()
    {
        _muzzleSpriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.05f);
        _muzzleSpriteRenderer.enabled = false;
    }

    #endregion

}
