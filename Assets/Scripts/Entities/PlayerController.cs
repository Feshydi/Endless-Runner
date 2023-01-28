using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private CharacterData _characterData;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private PlayerControls _inputActions;

    [Header("Generated data")]
    [SerializeField]
    private Vector2 _moveInput;

    [SerializeField]
    private Vector2 _mouseInput;

    [SerializeField]
    private bool _isMouseLeft;

    [SerializeField]
    private bool _isRolling;

    [SerializeField]
    private float _speedModifier;

    [Header("Animation Settings")]
    [SerializeField]
    private SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    private Animator _playerAnimator;

    [SerializeField]
    private SpriteRenderer _weaponSpriteRenderer;

    [SerializeField]
    private Animator _weaponAnimator;

    [Header("Additional")]
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void Awake()
    {
        _inputActions = new PlayerControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        AfterAnimation();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Look.performed += Look_performed;
        _inputActions.Player.Move.performed += Move_performed;
        _inputActions.Player.Roll.performed += Roll_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Look.performed -= Look_performed;
        _inputActions.Player.Move.performed -= Move_performed;
        _inputActions.Player.Roll.performed -= Roll_performed;
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        FlipByPointerPosition();
        WeaponLookAtPointerPosition();

        _playerAnimator.SetFloat("Speed", _moveInput.sqrMagnitude);
        _weaponAnimator.SetFloat("Speed", _moveInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveInput * _characterData.MoveSpeed * _speedModifier * Time.fixedDeltaTime);
    }

    private void Look_performed(InputAction.CallbackContext context)
    {
        _mouseInput = context.ReadValue<Vector2>();
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        if (_isRolling)
            return;

        _moveInput = context.ReadValue<Vector2>();
    }

    private void Roll_performed(InputAction.CallbackContext context)
    {
        if (_isRolling)
            return;

        _speedModifier = 3f;
        _isRolling = true;

        _playerAnimator.SetTrigger("DoRoll");
        _weaponAnimator.SetTrigger("DoRoll");
        _logger.Log($"{gameObject} rolling", this);
    }

    public void AfterAnimation()
    {
        _speedModifier = 1f;
        _isRolling = false;

        _moveInput = Vector2.zero;
        _mouseInput = Vector2.zero;
    }

    private void FlipByPointerPosition()
    {
        if (_isRolling)
            return;

        var mouseCoordinate = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (mouseCoordinate.x < transform.position.x)
        {
            if (!_isMouseLeft)
            {
                FlipLeft(true);
                _logger.Log($"{gameObject} looking at the right", this);
            }
        }
        else
        {
            if (_isMouseLeft)
            {
                FlipLeft(false);
                _logger.Log($"{gameObject} looking at the left", this);
            }
        }
    }

    private void FlipLeft(bool value)
    {
        _isMouseLeft = value;

        _playerSpriteRenderer.flipX = value;
        _weaponSpriteRenderer.flipX = value;
    }

    private void WeaponLookAtPointerPosition()
    {
        if (_isRolling)
            return;

        var mouseCoordinate = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        Vector3 difference = mouseCoordinate - _weaponSpriteRenderer.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        var curRotation = _weaponSpriteRenderer.transform.rotation;
        if (_isMouseLeft)
            _weaponSpriteRenderer.transform.rotation = Quaternion.Lerp(curRotation, Quaternion.Euler(0.0f, 0.0f, rotationZ - 180), 0.2f);
        else
            _weaponSpriteRenderer.transform.rotation = Quaternion.Lerp(curRotation, Quaternion.Euler(0.0f, 0.0f, rotationZ), 0.2f);
    }

    #endregion

}
