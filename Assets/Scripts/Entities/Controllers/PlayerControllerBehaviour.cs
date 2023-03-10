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

    [SerializeField]
    private BurstBehaviour _burstBehaviour;

    [Header("Additional")]
    [SerializeField]
    private PlayerAnimationController _animationController;

    [SerializeField]
    private PlayerAudioController _audioController;

    [SerializeField]
    private PlayerEffectController _effectController;

    #endregion

    #region Properties

    public Camera Camera
    {
        get => _camera;
        set => _camera = value;
    }

    public PlayerBaseState CurrentState
    {
        get => _currentState;
        set => _currentState = value;
    }

    public Vector2 PreviousMoveInput => _previousMoveInput;

    public Vector2 PreviousMouseInput => _previousMouseInput;

    public bool AttackInput => _attackInput;

    public HealthBehaviour HealthBehaviour => _healthBehaviour;

    public MoveBehaviour MoveBehaviour => _moveBehaviour;

    public LookBehaviour LookBehaviour => _lookBehaviour;

    public AttackBehaviour AttackBehaviour => _attackBehaviour;

    public RollBehaviour RollBehaviour => _rollBehaviour;

    public AbilityBehaviour AbilityBehaviour => _abilityBehaviour;

    public BurstBehaviour BurstBehaviour => _burstBehaviour;

    public PlayerAnimationController AnimationController => _animationController;

    public PlayerAudioController AudioController => _audioController;

    public PlayerEffectController EffectController => _effectController;

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
        _inputReader.BurstEvent += OnBurst;
        _inputReader.AttackEvent += OnAttackInitiated;
        _inputReader.AttackCanceledEvent += OnAttackCanceled;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
        _inputReader.MouseEvent -= OnMouseMove;
        _inputReader.RollEvent -= OnRoll;
        _inputReader.AbilityEvent -= OnAbility;
        _inputReader.BurstEvent -= OnBurst;
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
            _rollBehaviour.IsRollPressed = true;
    }

    private void OnAbility()
    {
        if (Time.time > _abilityBehaviour.NextAbilityTime)
        {
            _audioController.PlayAbilitySound();
            _abilityBehaviour.AbilityHandle(_effectController);
        }
    }

    private void OnBurst()
    {
        if (Time.time > _burstBehaviour.NextBurstTime)
            _burstBehaviour.BurstHandle(_effectController);
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
        if (_camera == null) _camera = Camera.main;
        if (_healthBehaviour == null) _healthBehaviour = this.GetComponent<HealthBehaviour>();
        if (_moveBehaviour == null) _moveBehaviour = this.GetComponent<MoveBehaviour>();
        if (_lookBehaviour == null) _lookBehaviour = this.GetComponent<LookBehaviour>();
        if (_rollBehaviour == null) _rollBehaviour = this.GetComponent<RollBehaviour>();
        if (_attackBehaviour == null) _attackBehaviour = this.GetComponent<AttackBehaviour>();
        if (_abilityBehaviour == null) _abilityBehaviour = this.GetComponent<AbilityBehaviour>();
        if (_burstBehaviour == null) _burstBehaviour = this.GetComponent<BurstBehaviour>();
    }

    #endregion

}
