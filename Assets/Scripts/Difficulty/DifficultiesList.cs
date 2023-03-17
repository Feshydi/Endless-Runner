using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Difficulties/Difficulties List")]
public class DifficultiesList : ScriptableObject
{

    #region Fields

    [SerializeField] private List<GameplayDifficulty> _difficulties;

    #endregion

    #region Methods

    public GameplayDifficulty GetGameplayDifficulty(Difficulty difficulty)
    {
        foreach (var gDifficulty in _difficulties)
        {
            if (gDifficulty.Difficulty.Equals(difficulty))
                return gDifficulty;
        }
        return null;
    }

    #endregion

}
