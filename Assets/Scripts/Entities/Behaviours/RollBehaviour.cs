using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollBehaviour : MonoBehaviour
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private CharacterData _characterData;

    [SerializeField]
    private HealthBehaviour _healthBehaviour;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _speedModifier;

    [Header("Generated")]
    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private Vector2 _rollDirection;

    [SerializeField]
    private float _cooldownTime;

    [SerializeField]
    private float _nextRollTime;

    public event Action<float> OnRollTimeEvent;

    #endregion

    #region

    public HealthBehaviour HealthBehaviour => _healthBehaviour;

    public float NextRollTime => _nextRollTime;

    #endregion

    #region Methods

    private void Start()
    {
        _moveSpeed = _characterData.MoveSpeed;
        _cooldownTime = _characterData.RollCooldownTime;
    }

    public void SetUpRoll(Vector2 moveInput, Vector2 mousePosition)
    {
        _nextRollTime = Time.time + _cooldownTime;
        _healthBehaviour.IsDamageAllowed = false;
        OnRollTimeEvent?.Invoke(_characterData.RollCooldownTime);

        // if input zero, then choose reverse direction by mouse position
        if (moveInput.Equals(Vector2.zero))
        {
            var difference = mousePosition - (Vector2)transform.position;
            _rollDirection = -difference.normalized;
        }
        else
            _rollDirection = moveInput;
    }

    public void RollHandle()
    {
        var move = _rollDirection * _moveSpeed * _speedModifier * Time.fixedDeltaTime;
        _rigidbody2D.AddForce(move);
    }

    public void AfterRoll()
    {
        _healthBehaviour.IsDamageAllowed = true;
    }

    #endregion
}
