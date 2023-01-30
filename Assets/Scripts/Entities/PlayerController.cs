using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : EntityController
{

    #region Fields

    [Header("Player Data")]
    [SerializeField]
    private CharacterData _characterData;

    [SerializeField]
    private PlayerControls _inputActions;

    [SerializeField]
    private Gun _gun;

    [Header("Generated data")]
    [SerializeField]
    private Vector2 _moveInput;

    [SerializeField]
    private Vector2 _mouseInput;

    [SerializeField]
    private Vector2 _lastInput;

    [SerializeField]
    private bool _isRolling;

    [SerializeField]
    private float _speedModifier;

    [Header("Animation")]
    [SerializeField]
    private Animator _weaponAnimator;

    [Header("Additional")]
    [SerializeField]
    private Camera _camera;

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();

        _inputActions = new PlayerControls();
        _speedModifier = 1f;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _inputActions.Player.Enable();
        _inputActions.Player.Look.performed += Look_performed;
        _inputActions.Player.Move.performed += Move_performed;
        _inputActions.Player.Roll.performed += Roll_performed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _inputActions.Player.Look.performed -= Look_performed;
        _inputActions.Player.Move.performed -= Move_performed;
        _inputActions.Player.Roll.performed -= Roll_performed;
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        WeaponLookAtPointerPosition();

        if (_inputActions.Player.Fire.IsPressed())
            _gun.Shoot();

        _entityAnimator.SetFloat("Speed", _moveInput.sqrMagnitude);
        _weaponAnimator.SetFloat("Speed", _moveInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        if (!_isRolling)
            _lastInput = _moveInput;

        _rigidbody2D.MovePosition(_rigidbody2D.position + _lastInput * _characterData.MoveSpeed * _speedModifier * Time.fixedDeltaTime);
    }

    #region Input Action

    private void Look_performed(InputAction.CallbackContext context)
    {
        _mouseInput = context.ReadValue<Vector2>();
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Roll_performed(InputAction.CallbackContext context)
    {
        if (_isRolling)
            return;

        _speedModifier = 3f;
        GetComponent<Collider2D>().enabled = false;
        _isRolling = true;

        _entityAnimator.SetTrigger("Roll");
        _weaponAnimator.SetTrigger("Roll");
    }

    #endregion

    public void AfterRollingAnimation()
    {
        _speedModifier = 1f;
        GetComponent<Collider2D>().enabled = true;
        _isRolling = false;
    }

    protected override void HealthEvent(float health)
    {
        base.HealthEvent(health);

        _weaponAnimator.SetFloat("Health", health);
        _weaponAnimator.SetTrigger("Hit");
    }

    protected override void AfterDeath()
    {
        base.AfterDeath();

        _moveInput = Vector2.zero;
        _mouseInput = Vector2.zero;
        enabled = false;
    }

    private void WeaponLookAtPointerPosition()
    {
        if (_isRolling)
            return;

        var mouseCoordinate = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector3 difference = mouseCoordinate - _weaponAnimator.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        _weaponAnimator.transform.rotation =
            Quaternion.Lerp(_weaponAnimator.transform.rotation, Quaternion.Euler(0.0f, 0.0f, rotationZ), 0.2f);
    }

    #endregion

}
