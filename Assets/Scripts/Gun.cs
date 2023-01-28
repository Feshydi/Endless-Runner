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
    private Transform _muzzlePosition;

    [Header("Generated Data")]
    [SerializeField]
    private float _nextShotTime;

    #endregion

    #region Methods

    private void Awake()
    {
        _inputActions = new PlayerControls();
    }

    private void OnEnable() => _inputActions.Player.Enable();

    private void OnDisable() => _inputActions.Player.Disable();

    private void Update()
    {
        if (_inputActions.Player.Fire.IsPressed())
            Shoot();
    }

    private void Shoot()
    {
        if (Time.time < _nextShotTime)
            return;
        _nextShotTime = Time.time + _characterData.DamageRate / 1000;

        var mouseCoordinate = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var shootDirection = mouseCoordinate - _muzzlePosition.position;

        var projectile = Instantiate(_projectilePrefab, _muzzlePosition.position, _muzzlePosition.rotation);
        projectile.Init(_characterData, shootDirection);
    }

    #endregion

}
