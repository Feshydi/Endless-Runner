using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, PlayerControls.IGameplayActions
{

    #region Fields

    public event UnityAction AttackEvent = delegate { };
    public event UnityAction AttackCanceledEvent = delegate { };
    public event UnityAction<Vector2> MouseEvent = delegate { };
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public event UnityAction RollEvent = delegate { };
    public event UnityAction AbilityEvent = delegate { };
    public event UnityAction BurstEvent = delegate { };
    public event UnityAction PauseMenuEvent = delegate { };

    private PlayerControls _inputActions;

    #endregion

    #region Methods

    private void OnEnable()
    {
        if (_inputActions is null)
        {
            _inputActions = new PlayerControls();
            _inputActions.Gameplay.SetCallbacks(this);
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
        if (context.canceled)
            AttackCanceledEvent?.Invoke();
    }

    public void OnMouse(InputAction.CallbackContext context)
    {
        if (context.performed)
            MouseEvent?.Invoke(context.ReadValue<Vector2>());
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

    public void OnBurst(InputAction.CallbackContext context)
    {
        if (context.performed)
            BurstEvent.Invoke();
    }

    public void OnPauseMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
            PauseMenuEvent.Invoke();
    }

    public void EnableGameplayInput()
    {
        _inputActions.Gameplay.Enable();
    }

    public void DisableAllInput()
    {
        _inputActions.Gameplay.Disable();
    }

    #endregion

}
