using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBehaviour : MonoBehaviour
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private InputReader _inputReader;

    [Header("Generated")]
    [SerializeField]
    private Vector2 _previousMoveInput;

    [SerializeField]
    private bool _isRollPressed;

    [SerializeField]
    private bool _isAbilityPressed;

    [Header("State Machine")]
    [SerializeField]
    private PlayerBaseState _currentState;

    [SerializeField]
    private PlayerStateFactory _stateFactory;

    [Header("Behaviours")]
    [SerializeField]
    private MoveBehaviour _moveBehaviour;

    [SerializeField]
    private AttackBehaviour _attackBehaviour;

    [SerializeField]
    private LookBehaviour _lookBehaviour;

    [SerializeField]
    private RollBehaviour _rollBehaviour;

    [SerializeField]
    private AbilityBehaviour _abilityBehaviour;

    [Header("Additional")]
    [SerializeField]
    private AnimationController _animationController;

    [SerializeField]
    private AudioController _audioController;

    #endregion

    #region Properties

    public PlayerBaseState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    public Vector2 PreviousMoveInput => _previousMoveInput;

    public bool IsRollPressed => _isRollPressed;

    public bool IsAbilityPressed => _isAbilityPressed;

    public MoveBehaviour MoveBehaviour => _moveBehaviour;

    public AttackBehaviour AttackBehaviour => _attackBehaviour;

    public RollBehaviour RollBehaviour => _rollBehaviour;

    public AbilityBehaviour AbilityBehaviour => _abilityBehaviour;

    public AnimationController AnimationController => _animationController;

    public AudioController AudioController => _audioController;

    #endregion

    #region Methods

    private void Start()
    {
        _stateFactory = new PlayerStateFactory(this);
        _currentState = _stateFactory.Idle();
        _currentState.OnStateEnter();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
        _inputReader.RollEvent += OnRoll;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.RollEvent -= OnRoll;
    }

    private void Update()
    {
        _currentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    private void OnMove(Vector2 moveInput)
    {
        _previousMoveInput = moveInput;
    }

    private void OnRoll()
    {
        _isRollPressed = true;
    }

    #endregion

}
