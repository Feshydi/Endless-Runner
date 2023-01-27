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
    private PlayerControls _inputActions;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private SpriteRenderer _playerSpriteRenderer;

    [SerializeField]
    private Camera _camera;

    [Header("Generated data")]
    [SerializeField]
    private Vector2 _moveInput;

    [SerializeField]
    private Vector2 _mouseInput;

    [SerializeField]
    private bool _isMouseLeft;

    [Header("Additional")]
    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void Awake()
    {
        _inputActions = new PlayerControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Look.performed += Look_performed;
        _inputActions.Player.Move.performed += Move_performed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Look.performed -= Look_performed;
        _inputActions.Player.Move.performed -= Move_performed;
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        FlipByPointerPosition();

        _animator.SetFloat("Speed", Mathf.Abs(_moveInput.magnitude));
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _moveInput * _characterData.MoveSpeed * Time.fixedDeltaTime);
    }

    private void Look_performed(InputAction.CallbackContext context)
    {
        _mouseInput = context.ReadValue<Vector2>();
    }

    private void Move_performed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void FlipByPointerPosition()
    {
        var mouseCoordinate = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (mouseCoordinate.x < transform.position.x)
        {
            if (!_isMouseLeft)
            {
                _isMouseLeft = true;
                _playerSpriteRenderer.flipX = true;
                _logger.Log($"{gameObject} looking at the left", this);
            }
        }
        else
        {
            if (_isMouseLeft)
            {
                _isMouseLeft = false;
                _playerSpriteRenderer.flipX = false;
                _logger.Log($"{gameObject} looking at the right", this);
            }
        }
    }

    #endregion

}
