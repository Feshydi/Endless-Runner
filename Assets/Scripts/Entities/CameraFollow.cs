using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    [SerializeField]
    private Tilemap _map;

    [SerializeField]
    private float _horizontalOffset;

    [SerializeField]
    private float _verticalOffset;

    #endregion

    #region Methods

    private void Start()
    {
        _smoothSpeed = 5f;
        _offset = new Vector3(0, 0, -10);
        _map.CompressBounds();
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = transform.position + _offset;
        if (desiredPosition.x < _horizontalOffset)
            desiredPosition.x = _horizontalOffset;
        else if (desiredPosition.x > _map.size.x - _horizontalOffset)
            desiredPosition.x = _map.size.x - _horizontalOffset;
        if (desiredPosition.y < _verticalOffset)
            desiredPosition.y = _verticalOffset;
        else if (desiredPosition.y > _map.size.y - _verticalOffset)
            desiredPosition.y = _map.size.y - _verticalOffset;

        Vector3 smoothedPosition = Vector3.Lerp(_camera.position, desiredPosition, _smoothSpeed * Time.fixedDeltaTime);
        _camera.position = smoothedPosition;
    }

    #endregion

}
