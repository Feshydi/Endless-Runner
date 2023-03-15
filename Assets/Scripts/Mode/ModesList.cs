using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Modes List")]
public class ModesList : ScriptableObject
{
    [SerializeField] private List<GameplayMode<float>> _modes;

    public List<GameplayMode<float>> Modes => _modes;
}
