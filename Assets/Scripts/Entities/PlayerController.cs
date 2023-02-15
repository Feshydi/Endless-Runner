using System;
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
    private Vector2 _lastMoveInput;

    [SerializeField]
    private Vector2 _lastMousePosition;

    [SerializeField]
    private bool _isRolling;

    [SerializeField]
    private bool _isLookingLeft;

    [SerializeField]
    private float _speedModifier;

    [SerializeField]
    private float _nextRollTime;

    [SerializeField]
    private float _nextSkillTime;

    [Header("Animation")]
    [SerializeField]
    private Animator _weaponAnimator;

    [Header("Additional")]
    [SerializeField]
    private Camera _camera;

    public event Action<float, float> OnHealthChanged;

    public event Action<float> OnRollTimeChanged;

    public event Action<float> OnSkillTimeChanged;

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();

        _inputActions = new PlayerControls();
        _entityAnimator.SetFloat("Health", _characterData.HealthPoints);
        _speedModifier = 1f;

        _lastMoveInput = Vector2.zero;
        _lastMousePosition = Vector2.zero;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _inputActions.Player.Enable();
        _inputActions.Player.Move.performed += Move_performed;
        _inputActions.Player.Roll.performed += Roll_performed;
        _inputActions.Player.Skill.performed += Skill_performed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _inputActions.Player.Move.performed -= Move_performed;
        _inputActions.Player.Roll.performed -= Roll_performed;
        _inputActions.Player.Skill.performed -= Skill_performed;
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        WeaponLookAtPointerPosition();

        ShootHandle();
    }

    private void FixedUpdate()
    {
        MoveHandle();
    }

    #region Input Action

    private void Move_performed(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Roll_performed(InputAction.CallbackContext context)
    {
        if (Time.time < _nextRollTime || _isRolling || GameManager.Instance.GameMode.Equals(GameMode.PauseMenu))
            return;

        _nextRollTime = Time.time + _characterData.RollCooldownTime;
        OnRollTimeChanged?.Invoke(_characterData.RollCooldownTime);

        _speedModifier = 3f;
        GetComponent<Collider2D>().enabled = false;
        _isRolling = true;

        if (_lastMoveInput.Equals(Vector2.zero))
        {
            var difference = _lastMousePosition - (Vector2)transform.position;
            _lastMoveInput = difference.normalized;     // choosing roll direction by mouse position
        }

        _entityAnimator.SetTrigger("Roll");
        _weaponAnimator.SetTrigger("Roll");
    }

    private void Skill_performed(InputAction.CallbackContext context)
    {
        if (Time.time < _nextSkillTime || _isRolling || GameManager.Instance.GameMode.Equals(GameMode.PauseMenu))
            return;

        _nextSkillTime = Time.time + _characterData.ExplosionCooldownTime;
        OnSkillTimeChanged?.Invoke(_characterData.ExplosionCooldownTime);

        var explosionPos = (Vector2)transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, _characterData.ExplosionRadius);
        foreach (var hit in colliders)
        {
            StartCoroutine(ExplosionHandle(hit));
        }
    }

    #endregion

    private IEnumerator ExplosionHandle(Collider2D collider)
    {
        if (collider.TryGetComponent(out Rigidbody2D rigidbody2D)
            && collider.TryGetComponent(out EnemyController entity)
            && collider.TryGetComponent(out IDamageable damageable))
        {
            entity.OnHitStart();
            var direction = (collider.transform.position - transform.position).normalized;
            rigidbody2D.AddForce(direction * _characterData.ExplosionForce, ForceMode2D.Impulse);
            yield return new WaitForSeconds(0.2f);
            damageable.DoDamage(_characterData.ExplosionDamage);
        }
    }

    private void ShootHandle()
    {
        if (_isRolling || _isHit || GameManager.Instance.GameMode.Equals(GameMode.PauseMenu))
            return;

        if (_inputActions.Player.Fire.IsPressed())
            _gun.Shoot();
    }

    private void MoveHandle()
    {
        if (_isHit || GameManager.Instance.GameMode.Equals(GameMode.PauseMenu))
            return;

        if (!_isRolling)
            _lastMoveInput = _moveInput;

        if (_lastMoveInput == Vector2.zero)     // return if player not moving
        {
            _entityAnimator.SetFloat("Speed", 0f);
            _weaponAnimator.SetFloat("Speed", 0f);
            return;
        }

        _rigidbody2D.MovePosition(_rigidbody2D.position + _lastMoveInput * _characterData.MoveSpeed * _speedModifier * Time.fixedDeltaTime);

        _entityAnimator.SetFloat("Speed", _moveInput.sqrMagnitude);
        _weaponAnimator.SetFloat("Speed", _moveInput.sqrMagnitude);
    }

    private void WeaponLookAtPointerPosition()
    {
        if (_isRolling || _isHit || GameManager.Instance.GameMode.Equals(GameMode.PauseMenu))
            return;

        Vector2 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (_lastMousePosition.Equals(mousePosition))      // return if mouse not moving
            return;

        _lastMousePosition = mousePosition;

        var difference = _lastMousePosition - (Vector2)_weaponAnimator.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        _weaponAnimator.transform.rotation =
            Quaternion.Lerp(_weaponAnimator.transform.rotation, Quaternion.Euler(0.0f, 0.0f, rotationZ), 0.2f);
    }

    public void AfterRollingAnimation()
    {
        _speedModifier = 1f;
        GetComponent<Collider2D>().enabled = true;
        _isRolling = false;
    }

    protected override void HealthEvent(float health)
    {
        base.HealthEvent(health);

        OnHealthChanged?.Invoke(health, _characterData.HealthPoints);
        _weaponAnimator.SetFloat("Health", health);
        _weaponAnimator.SetTrigger("Hit");
    }

    private void AfterDeath()
    {
        _moveInput = Vector2.zero;
        enabled = false;

        GameManager.Instance.SetGameMode(GameMode.PauseMenu);
    }

    #endregion

}
