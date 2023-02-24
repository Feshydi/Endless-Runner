using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookBehaviour : MonoBehaviour
{

    #region Fields

    [Header("Generated")]
    [SerializeField]
    private Transform _objectToRotate;

    #endregion

    #region Methods

    public void LookAtHandle(Vector2 position)
    {
        var difference = position - (Vector2)_objectToRotate.position;
        var rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        _objectToRotate.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    #endregion

}
