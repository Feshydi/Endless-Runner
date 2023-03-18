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
        _playerController.AudioController.PlayHitSound();
    }

    public override void OnUpdate()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        if (!_playerController.HealthBehaviour.IsHitted)
        {
            if (_playerController.HealthBehaviour.IsDead)
                SwitchState(_stateFactory.Death());
            else
            {
                if (_playerController.PreviousMoveInput.Equals(Vector2.zero))
                    SwitchState(_stateFactory.Idle());
                else
                    SwitchState(_stateFactory.Move());
            }
        }
    }

    #endregion

}
