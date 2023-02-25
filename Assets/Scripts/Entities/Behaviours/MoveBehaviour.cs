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

    #endregion

    #region Methods

    private void Start()
    {
        _moveSpeed = _characterData.MoveSpeed;
    }

    public void MoveHandle(Vector2 moveInput)
    {
        _rigidbody2D.velocity = moveInput * _moveSpeed * Time.fixedDeltaTime;
    }

    #endregion

}