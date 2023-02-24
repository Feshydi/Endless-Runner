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
        _playerController.AnimationController.PlayIdleAnimation();
    }

    public override void OnUpdate()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (!_playerController.PreviousMoveInput.Equals(Vector2.zero))
            SwitchState(_stateFactory.Move());

        //if (_playerController.IsRollPressed)
        //    SwitchState(_stateFactory.Roll());

        //if (_playerController.IsAbilityPressed)
        //    SwitchState(_stateFactory.Ability());
    }

    #endregion

}
