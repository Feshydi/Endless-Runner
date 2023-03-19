using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerUIManager : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private GameObject _pauseMenu;

    [SerializeField]
    private GameObject _gameoverMenu;

    [SerializeField]
    private InputReader _inputReader;

    [SerializeField]
    private PlayerControllerBehaviour _player;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private Slider _rollBar;

    [SerializeField]
    private Slider _skillBar;

    [SerializeField]
    private Slider _burstBar;

    [SerializeField]
    private Logger _logger;

    [Header("Events")]
    public FloatEventChannel OnScorePointsEvent;

    #endregion

    #region Methods

    private void Awake()
    {
        _scoreText.text = "Score: 0";
        _healthBar.value = 1f;
        _rollBar.value = 1f;
        _skillBar.value = 1f;
        _burstBar.value = 1f;

        _logger.Log($"{gameObject} created", this);
    }

    public void Init(PlayerControllerBehaviour player)
    {
        _player = player;
        _inputReader.PauseMenuEvent += PauseMenu_performed;
        _player.HealthBehaviour.OnHealthValueEvent += NewHealthPoints;
        _player.RollBehaviour.OnRollTimeEvent += RollBarProgress;
        _player.AbilityBehaviour.OnAbilityTimeEvent += SkillBarProgress;
        _player.BurstBehaviour.OnBurstTimeEvent += BurstBarProgress;
        OnScorePointsEvent.OnEventRaised += NewScore;
    }

    private void OnDestroy()
    {
        _inputReader.PauseMenuEvent -= PauseMenu_performed;
        _player.HealthBehaviour.OnHealthValueEvent -= NewHealthPoints;
        _player.RollBehaviour.OnRollTimeEvent -= RollBarProgress;
        _player.AbilityBehaviour.OnAbilityTimeEvent -= SkillBarProgress;
        _player.BurstBehaviour.OnBurstTimeEvent -= BurstBarProgress;
        OnScorePointsEvent.OnEventRaised -= NewScore;
    }

    public void NewScore(float value)
    {
        _scoreText.text = string.Concat("Score: ", value);

        _logger.Log($"New Score: {_scoreText.text}", this);
    }

    public void NewHealthPoints(float currentHP, float maxHP)
    {
        _healthBar.value = currentHP / maxHP;

        _logger.Log($"New Health Points: {currentHP}", this);

        if (currentHP == 0)
        {
            _gameoverMenu.SetActive(true);
            return;
        }
    }

    void RollBarProgress(float value)
    {
        LeanTween.value(gameObject, SetRollBarProgress, 0, 1, value);
    }

    public void SetRollBarProgress(float value)
    {
        _rollBar.value = value;

        if (_rollBar.value == 1)
            _logger.Log("Roll is ready", this);
    }

    void SkillBarProgress(float value)
    {
        LeanTween.value(gameObject, SetSkillBarProgress, 0, 1, value);
    }

    public void SetSkillBarProgress(float value)
    {
        _skillBar.value = value;

        if (_skillBar.value == 1)
            _logger.Log("Skill is ready", this);
    }

    void BurstBarProgress(float value)
    {
        LeanTween.value(gameObject, SetBurstBarProgress, 0, 1, value);
    }

    public void SetBurstBarProgress(float value)
    {
        _burstBar.value = value;

        if (_burstBar.value == 1)
            _logger.Log("Burst is ready", this);
    }

    private void PauseMenu_performed()
    {
        GameManager.Instance.SetGameMode(GameMode.PauseMenu);
        _pauseMenu.SetActive(true);
    }

    #endregion

}
