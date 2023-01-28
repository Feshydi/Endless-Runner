using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    #region Properties

    [Header("Camera Settings")]
    [SerializeField]
    private Transform _camera;

    [SerializeField]
    private float _smoothSpeed;

    [SerializeField]
    private Vector3 _offset;

    #endregion

    #region Methods

    private void Awake()
    {
        _smoothSpeed = 5f;
        _offset = new Vector3(0, 0, -10);
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = transform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(_camera.position, desiredPosition, _smoothSpeed * Time.fixedDeltaTime);
        _camera.position = smoothedPosition;
    }

    #endregion

}
