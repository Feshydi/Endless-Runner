using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayerControls.IPlayerActions
{

    #region Fields

    public event UnityAction AttackEvent = delegate { };
    public event UnityAction LookEvent = delegate { };
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction RollEvent = delegate { };
    public event UnityAction AbilityEvent = delegate { };

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
            AttackEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
            LookEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        if (context.performed)
            RollEvent.Invoke();
    }

    public void OnAbility(InputAction.CallbackContext context)
    {
        if (context.performed)
            AbilityEvent.Invoke();
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
