using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerBehaviour : MonoBehaviour
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private InputReader _inputReader;

    [SerializeField]
    private Camera _camera;

    [Header("Generated")]
    [SerializeField]
    private Vector2 _previousMoveInput;

    [SerializeField]
    private Vector2 _previousMouseInput;

    [SerializeField]
    private bool _isRollPressed;

    [SerializeField]
    private bool _isAbilityPressed;

    [SerializeField]
    private bool _attackInput;

    [Header("State Machine")]
    [SerializeField]
    private PlayerBaseState _currentState;

    [SerializeField]
    private PlayerStateFactory _stateFactory;

    [Header("Behaviours")]
    [SerializeField]
    private HealthBehaviour _healthBehaviour;

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

    public Vector2 PreviousMouseInput => _previousMouseInput;

    public bool IsRollPressed
    {
        get => _isRollPressed;
        set => _isRollPressed = value;
    }

    public bool IsAbilityPressed
    {
        get => _isAbilityPressed;
        set => _isAbilityPressed = value;
    }

    public bool AttackInput => _attackInput;

    public HealthBehaviour HealthBehaviour => _healthBehaviour;

    public MoveBehaviour MoveBehaviour => _moveBehaviour;

    public LookBehaviour LookBehaviour => _lookBehaviour;

    public AttackBehaviour AttackBehaviour => _attackBehaviour;

    public RollBehaviour RollBehaviour => _rollBehaviour;

    public AbilityBehaviour AbilityBehaviour => _abilityBehaviour;

    public AnimationController AnimationController => _animationController;

    public AudioController AudioController => _audioController;

    #endregion

    #region Methods

    private void Start()
    {
        CheckFields();
        _stateFactory = new PlayerStateFactory(this);
        _currentState = _stateFactory.Idle();
        _currentState.OnStateEnter();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
        _inputReader.MouseEvent += OnMouseMove;
        _inputReader.RollEvent += OnRoll;
        _inputReader.AbilityEvent += OnAbility;
        _inputReader.AttackEvent += OnAttackInitiated;
        _inputReader.AttackCanceledEvent += OnAttackCanceled;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.MouseEvent += OnMouseMove;
        _inputReader.RollEvent -= OnRoll;
        _inputReader.AbilityEvent -= OnAbility;
        _inputReader.AttackEvent -= OnAttackInitiated;
        _inputReader.AttackCanceledEvent -= OnAttackCanceled;
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

    private void OnMouseMove(Vector2 mouseInput)
    {
        _previousMouseInput = _camera.ScreenToWorldPoint(mouseInput);
    }

    private void OnRoll()
    {
        if (Time.time > _rollBehaviour.NextRollTime)
            _isRollPressed = true;
    }

    private void OnAbility()
    {
        if (Time.time > _abilityBehaviour.NextAbilityTime)
            _isAbilityPressed = true;
    }

    private void OnAttackInitiated()
    {
        _attackInput = true;
    }

    private void OnAttackCanceled()
    {
        _attackInput = false;
    }

    private void CheckFields()
    {
        if (_healthBehaviour is null) _healthBehaviour = this.GetComponent<HealthBehaviour>();
        if (_moveBehaviour is null) _moveBehaviour = this.GetComponent<MoveBehaviour>();
        if (_lookBehaviour is null) _lookBehaviour = this.GetComponent<LookBehaviour>();
        if (_rollBehaviour is null) _rollBehaviour = this.GetComponent<RollBehaviour>();
        if (_attackBehaviour is null) _attackBehaviour = this.GetComponent<AttackBehaviour>();
        if (_abilityBehaviour is null) _abilityBehaviour = this.GetComponent<AbilityBehaviour>();
    }

    #endregion

}
