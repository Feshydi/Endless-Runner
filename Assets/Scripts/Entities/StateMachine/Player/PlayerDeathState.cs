using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{

    #region Methods

    public PlayerDeathState(PlayerControllerBehaviour playerController, PlayerStateFactory stateFactory)
        : base(playerController, stateFactory) { }

    public override void OnStateEnter()
    {
        _playerController.HealthBehaviour.IsDamageAllowed = false;
        _playerController.MoveBehaviour.MoveHandle(Vector2.zero);

        GameManager.Instance?.SetGameMode(GameMode.PauseMenu);
    }

    public override void OnUpdate() { }

    public override void CheckSwitchState() { }

    #endregion

}
