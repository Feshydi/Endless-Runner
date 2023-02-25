using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerBaseState
{

    #region Methods

    public PlayerAbilityState(PlayerControllerBehaviour playerController, PlayerStateFactory stateFactory)
        : base(playerController, stateFactory) { }

    public override void OnStateEnter()
    {
        _playerController.AudioController.PlayAbilitySound();
        _playerController.AbilityBehaviour.SetUpAbility();
        _playerController.AbilityBehaviour.AbilityHandle();
    }

    public override void OnStateExit()
    {
        _playerController.IsAbilityPressed = false;
    }

    public override void OnUpdate()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (_playerController.PreviousMoveInput.Equals(Vector2.zero))
            SwitchState(_stateFactory.Idle());
        else
            SwitchState(_stateFactory.Move());
    }

    #endregion

}
