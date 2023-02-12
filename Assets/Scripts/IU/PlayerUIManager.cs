using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.U2D.Animation;
using static UnityEngine.Rendering.DebugUI;

public class PlayerUIManager : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private Slider _healthBar;

    [SerializeField]
    private Slider _rollBar;

    [SerializeField]
    private Slider _skillBar;

    [SerializeField]
    private Logger _logger;

    #endregion

    #region Methods

    private void Awake()
    {
        _scoreText.text = "Score: 0";
        _healthBar.value = 1f;
        _rollBar.value = 1f;
        _skillBar.value = 1f;

        _logger.Log($"{gameObject} created", this);
    }

    private void OnEnable()
    {
        _player.OnHealthChanged += NewHealthPoints;
        _player.OnRollTimeChanged += RollBarProgress;
        _player.OnSkillTimeChanged += SkillBarProgress;
        ScoreManager.OnScorePointsChanged += NewScore;
    }

    private void OnDisable()
    {
        _player.OnHealthChanged -= NewHealthPoints;
        _player.OnRollTimeChanged -= RollBarProgress;
        _player.OnSkillTimeChanged -= SkillBarProgress;
        ScoreManager.OnScorePointsChanged -= NewScore;
    }

    public void NewScore(int value)
    {
        _scoreText.text = string.Concat("Score: ", value);

        _logger.Log($"New Score: {_scoreText.text}", this);
    }

    public void NewHealthPoints(float currentHP, float maxHP)
    {
        _healthBar.value = currentHP / maxHP;

        _logger.Log($"New Health Points: {currentHP}", this);
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

    #endregion

}
