using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Difficulties/Difficulties List")]
public class DifficultiesList : ScriptableObject
{
    [SerializeField] private List<GameplayDifficulty> _difficulties;

    public List<GameplayDifficulty> Difficulties => _difficulties;
}
