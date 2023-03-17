using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelLoadingDescription : MonoBehaviour
{

    #region Fields

    [Header("Data")]
    [SerializeField]
    private GameplayManager _gameplayManager;

    [SerializeField]
    private TextMeshProUGUI _modeRulesText;

    [SerializeField]
    private TextMeshProUGUI _rollText;

    [SerializeField]
    private TextMeshProUGUI _abilityText;

    [SerializeField]
    private TextMeshProUGUI _burstText;

    #endregion

    #region Methods

    public void UpdateText()
    {
        _modeRulesText.text = _gameplayManager.GetGameplayModeStats().Description;
        _rollText.text = _gameplayManager.Player.CharacterData.RollDescription;
        _abilityText.text = _gameplayManager.Player.CharacterData.ExplosionDescription;
        _burstText.text = _gameplayManager.Player.CharacterData.BurstDescription;
    }

    #endregion

}
