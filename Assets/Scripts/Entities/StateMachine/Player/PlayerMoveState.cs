using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{

    #region Methods

    public PlayerMoveState(PlayerControllerBehaviour playerController, PlayerStateFactory stateFactory)
        : base(playerController, stateFactory) { }

    public override void OnStateEnter()
    {
        _playerController.AnimationController.PlayMoveAnimation();
    }

    public override void OnStateExit()
    {
        _playerController.MoveBehaviour.MoveHandle(Vector2.zero);
    }

    public override void OnUpdate()
    {
        CheckSwitchState();
    }

    public override void OnFixedUpdate()
    {
        _playerController.MoveBehaviour.MoveHandle(_playerController.PreviousMoveInput);
    }

    public override void CheckSwitchState()
    {
        if (_playerController.PreviousMoveInput.Equals(Vector2.zero))
            SwitchState(_stateFactory.Idle());
    }

    #endregion

}
