using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{

    #region Fields

    [SerializeField] private PlayerControllerBehaviour _playerController;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private float _updateInterval;
    [SerializeField] private bool _flipByX;
    [SerializeField] private bool _flipSprite;
    [SerializeField] private bool _flipScale;
    private bool _isFacingLeft;

    private Vector3 _startScale;

    #endregion

    #region Methods

    private void Start()
    {
        _startScale = transform.localScale;

        StartCoroutine(UpdatePeriodically());
    }

    private IEnumerator UpdatePeriodically()
    {
        while (true)
        {
            if (_flipSprite)
                FlipSprite();
            if (_flipScale)
                FlipScale();
            yield return new WaitForSeconds(_updateInterval);
        }
    }

    private void FlipSprite()
    {
        bool isLeft = IsMousePositionLeft(_playerController.PreviousMouseInput);

        if (isLeft && !_isFacingLeft)
        {
            _isFacingLeft = true;
            if (_flipByX)
                _spriteRenderer.flipX = true;
            else
                _spriteRenderer.flipY = true;
        }
        else if (!isLeft && _isFacingLeft)
        {
            _isFacingLeft = false;
            if (_flipByX)
                _spriteRenderer.flipX = false;
            else
                _spriteRenderer.flipY = false;
        }
    }

    private void FlipScale()
    {
        bool isLeft = IsMousePositionLeft(_playerController.PreviousMouseInput);

        if (isLeft && !_isFacingLeft)
        {
            _isFacingLeft = true;
            if (_flipByX)
                transform.localScale = new Vector3(-_startScale.x, _startScale.y, 1f);
            else
                transform.localScale = new Vector3(_startScale.x, -_startScale.y, 1f);
        }
        else if (!isLeft && _isFacingLeft)
        {
            _isFacingLeft = false;
            if (_flipByX)
                transform.localScale = new Vector3(_startScale.x, _startScale.y, 1f);
            else
                transform.localScale = new Vector3(_startScale.x, _startScale.y, 1f);
        }
    }

    private bool IsMousePositionLeft(Vector2 point) =>
        point.x < transform.position.x;

    #endregion

}
