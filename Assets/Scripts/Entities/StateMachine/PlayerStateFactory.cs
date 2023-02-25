using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{

    #region Fields

    PlayerControllerBehaviour _playerController;

    #endregion

    #region Methods

    public PlayerStateFactory(PlayerControllerBehaviour playerController)
    {
        _playerController = playerController;
    }

    public PlayerBaseState Idle() => new PlayerIdleState(_playerController, this);

    public PlayerBaseState Move() => new PlayerMoveState(_playerController, this);

    public PlayerBaseState Roll() => new PlayerRollState(_playerController, this);

    public PlayerBaseState Ability() => new PlayerAbilityState(_playerController, this);

    public PlayerBaseState Death() => new PlayerDeathState(_playerController, this);

    public PlayerBaseState Hit() => new PlayerHitState(_playerController, this);

    #endregion

}
