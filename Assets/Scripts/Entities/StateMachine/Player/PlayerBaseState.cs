public abstract class PlayerBaseState
{

    #region Fields

    protected readonly PlayerControllerBehaviour _playerController;

    protected PlayerStateFactory _stateFactory;

    #endregion

    #region Methods

    public PlayerBaseState(PlayerControllerBehaviour playerController, PlayerStateFactory stateFactory)
    {
        _playerController = playerController;
        _stateFactory = stateFactory;
    }

    public virtual void OnStateEnter() { }

    public virtual void OnStateExit() { }

    public abstract void OnUpdate();

    public virtual void OnFixedUpdate() { }

    public abstract void CheckSwitchState();

    protected void SwitchState(PlayerBaseState newState)
    {
        OnStateExit();
        newState.OnStateEnter();
        _playerController.CurrentState = newState;
    }

    #endregion

}
