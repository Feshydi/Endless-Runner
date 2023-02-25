using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{

    #region Methods

    public PlayerHitState(PlayerControllerBehaviour playerController, PlayerStateFactory stateFactory)
        : base(playerController, stateFactory) { }

    public override void OnStateEnter()
    {
        _playerController.MoveBehaviour.MoveHandle(Vector2.zero);
        _playerController.AnimationController.PlayHitAnimation(_playerController.HealthBehaviour.HealthPoints);
    }

    public override void OnUpdate()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (!_playerController.HealthBehaviour.IsHitted)
            SwitchState(_stateFactory.Idle());
    }

    #endregion

}
