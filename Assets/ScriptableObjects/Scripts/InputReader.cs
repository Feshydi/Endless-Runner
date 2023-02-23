using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayerControls.IPlayerActions
{

    #region Fields

    public event UnityAction _attackEvent = delegate { };
    public event UnityAction _lookEvent = delegate { };
    public event UnityAction<Vector2> _moveEvent = delegate { };
    public event UnityAction _rollEvent = delegate { };
    public event UnityAction _skillEvent = delegate { };

    private PlayerControls _inputActions;

    #endregion

    #region Methods

    private void OnEnable()
    {
        if (_inputActions is null)
        {
            _inputActions = new PlayerControls();
            _inputActions.Player.SetCallbacks(this);
        }

        EnableGameplayInput();
    }

    private void OnDisable()
    {
        DisableAllInput();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            _attackEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
            _lookEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            _moveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
            _rollEvent.Invoke();
    }

    public void OnSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
            _skillEvent.Invoke();
    }

    public void EnableGameplayInput()
    {
        _inputActions.Player.Enable();
    }

    public void DisableAllInput()
    {
        _inputActions.Player.Disable();
    }

    #endregion

}
