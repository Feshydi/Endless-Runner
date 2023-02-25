using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    #region Methods

    public PlayerIdleState(PlayerControllerBehaviour playerController, PlayerStateFactory stateFactory)
        : base(playerController, stateFactory) { }

    public override void OnStateEnter()
    {
        _playerController.MoveBehaviour.MoveHandle(Vector2.zero);
        _playerController.AnimationController.PlayIdleAnimation();
    }

    public override void OnUpdate()
    {
        CheckSwitchState();
        _playerController.LookBehaviour.LookAtMouseHandle();

        if (_playerController.AttackInput)
            _playerController.AttackBehaviour.ShootHandle();
    }

    public override void CheckSwitchState()
    {
        if (_playerController.HealthBehaviour.IsDead)
            SwitchState(_stateFactory.Death());

        if (!_playerController.PreviousMoveInput.Equals(Vector2.zero))
            SwitchState(_stateFactory.Move());

        if (_playerController.IsRollPressed)
            SwitchState(_stateFactory.Roll());

        if (_playerController.IsAbilityPressed)
            SwitchState(_stateFactory.Ability());
    }

    #endregion

}
