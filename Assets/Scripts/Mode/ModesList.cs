using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Modes List")]
public class ModesList : ScriptableObject
{

    #region Fields

    [SerializeField] private List<GameplayMode<float>> _modes;

    #endregion

    #region Methods

    public void Init(Mode mode)
    {
        foreach (var gMode in _modes)
        {
            gMode.IsEnabled = gMode.Equals(mode);
            gMode.Init();
        }
    }

    public GameplayMode<float> GetGameplayModeStats(Mode mode)
    {
        foreach (var gMode in _modes)
        {
            if (gMode.Mode.Equals(mode))
                return gMode;
        }
        return null;
    }

    #endregion

}
