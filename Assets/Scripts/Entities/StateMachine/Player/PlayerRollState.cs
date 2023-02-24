using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerBaseState
{

    #region Methods

    public PlayerRollState(PlayerControllerBehaviour playerController, PlayerStateFactory stateFactory)
        : base(playerController, stateFactory) { }

    public override void OnUpdate()
    {
        //_playerController.

        //_playerController.RollBehaviour.RollHandle();
    }

    public override void CheckSwitchState()
    {
        throw new System.NotImplementedException();
    }

    #endregion

}
