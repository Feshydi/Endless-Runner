using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerBaseState
{

    #region Methods

    public PlayerRollState(PlayerControllerBehaviour playerController, PlayerStateFactory stateFactory)
        : base(playerController, stateFactory) { }

    public override void OnStateEnter()
    {
        _playerController.RollBehaviour.SetUpRoll(_playerController.PreviousMoveInput, _playerController.PreviousMouseInput);
        _playerController.AnimationController.PlayRollAnimation();
    }

    public override void OnStateExit()
    {
        _playerController.IsRollPressed = false;
    }

    public override void OnUpdate()
    {
        CheckSwitchState();
    }

    public override void OnFixedUpdate()
    {
        _playerController.RollBehaviour.RollHandle();
    }

    public override void CheckSwitchState()
    {
        if (_playerController.HealthBehaviour.IsDamageAllowed)
            SwitchState(_stateFactory.Idle());
    }

    #endregion

}
