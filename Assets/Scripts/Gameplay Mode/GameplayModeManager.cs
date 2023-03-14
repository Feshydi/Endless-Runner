using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Game Mode Manager")]
public class GameplayModeManager : ScriptableObject
{

    #region Fields

    [SerializeField] private List<GameplayMode<float>> _modes;

    #endregion

    #region Methods

    public void InitModes()
    {
        foreach (var mode in _modes)
            mode.Init();
    }

    #endregion

}
