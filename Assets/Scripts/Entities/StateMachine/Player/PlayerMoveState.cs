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

    public override void OnUpdate()
    {
        CheckSwitchState();
        _playerController.LookBehaviour.LookAtMouseHandle(_playerController.PreviousMouseInput);

        if (_playerController.AttackInput)
            _playerController.AttackBehaviour.ShootHandle();
    }

    public override void OnFixedUpdate()
    {
        _playerController.MoveBehaviour.MoveHandle(_playerController.PreviousMoveInput);
    }

    public override void CheckSwitchState()
    {
        if (_playerController.HealthBehaviour.IsDead)
            SwitchState(_stateFactory.Death());

        if (_playerController.RollBehaviour.IsRollPressed)
            SwitchState(_stateFactory.Roll());

        if (_playerController.PreviousMoveInput.Equals(Vector2.zero))
            SwitchState(_stateFactory.Idle());

        if (_playerController.HealthBehaviour.IsHitted)
            SwitchState(_stateFactory.Hit());
    }

    #endregion

}
