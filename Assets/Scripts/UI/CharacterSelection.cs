using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CharacterModel
{
    public Animator Model;
    public PlayerControllerBehaviour Character;
}

public class CharacterSelection : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private GameplayManager _gameplayManager;

    [SerializeField]
    private CharacterModel[] _characterModels;

    [SerializeField]
    private int _selectedCharacterIndex;

    #endregion

    #region Methods

    private void OnEnable()
    {
        SetDefaultCharacter();
    }

    private void SetDefaultCharacter()
    {
        foreach (var item in _characterModels)
        {
            item.Model.gameObject.SetActive(false);
        }
        _selectedCharacterIndex = 0;
        _gameplayManager.Player = _characterModels[_selectedCharacterIndex].Character;
        _characterModels[_selectedCharacterIndex].Model.gameObject.SetActive(true);
    }

    public void NextCharacter()
    {
        _characterModels[_selectedCharacterIndex].Model.gameObject.SetActive(false);
        _selectedCharacterIndex = (_selectedCharacterIndex + 1) % _characterModels.Length;
        _characterModels[_selectedCharacterIndex].Model.gameObject.SetActive(true);
        _gameplayManager.Player = _characterModels[_selectedCharacterIndex].Character;
    }

    public void PreviousCharacter()
    {
        _characterModels[_selectedCharacterIndex].Model.gameObject.SetActive(false);
        _selectedCharacterIndex--;
        if (_selectedCharacterIndex < 0)
            _selectedCharacterIndex += _characterModels.Length;
        _characterModels[_selectedCharacterIndex].Model.gameObject.SetActive(true);
        _gameplayManager.Player = _characterModels[_selectedCharacterIndex].Character;
    }

    #endregion

}
