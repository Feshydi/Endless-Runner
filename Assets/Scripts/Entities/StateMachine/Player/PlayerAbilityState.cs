using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerBaseState
{

    #region Methods

    public PlayerAbilityState(PlayerControllerBehaviour playerController, PlayerStateFactory stateFactory)
        : base(playerController, stateFactory) { }

    public override void OnStateEnter()
    {
        _playerController.AudioController.PlayAbilitySound();
        if (_playerController.BurstBehaviour.IsBurst)
            _playerController.EffectController.EnableAbilityEffect();
        _playerController.AbilityBehaviour.SetUpAbility();
        _playerController.AbilityBehaviour.AbilityHandle();
    }

    public override void OnStateExit()
    {
        _playerController.IsAbilityPressed = false;
        _playerController.EffectController.DisableAbilityEffect();
    }

    public override void OnUpdate()
    {
        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        SwitchState(_stateFactory.Idle());
    }

    #endregion

}
