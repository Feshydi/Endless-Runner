using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehaviour : MonoBehaviour
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private CharacterData _characterData;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [Header("Generated")]
    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _maxSpeed;

    #endregion

    #region Methods

    private void Start()
    {
        _moveSpeed = _characterData.MoveSpeed;
        _maxSpeed = _characterData.MaxMoveSpeed;
    }

    public void MoveHandle(Vector2 moveInput)
    {
        var move = moveInput * _moveSpeed * Time.fixedDeltaTime;
        _rigidbody2D.AddForce(move);
        if (_rigidbody2D.velocity.magnitude > _maxSpeed)
            _rigidbody2D.velocity = Vector2.ClampMagnitude(_rigidbody2D.velocity, _maxSpeed);
    }

    #endregion

}
