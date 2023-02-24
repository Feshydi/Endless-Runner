using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollBehaviour : MonoBehaviour
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private CharacterData _characterData;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    private float _speedModifier;

    [Header("Generated")]
    [SerializeField]
    private float _moveSpeed;

    #endregion

    #region Methods

    private void Start()
    {
        _moveSpeed = _characterData.MoveSpeed;
    }

    public void RollHandle(Vector2 rollDirection)
    {
        _rigidbody2D.velocity = rollDirection.normalized * _moveSpeed * _speedModifier * Time.deltaTime;
    }

    #endregion
}
