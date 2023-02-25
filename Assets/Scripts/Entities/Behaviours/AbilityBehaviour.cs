using System;
using UnityEngine;

public abstract class AbilityBehaviour : MonoBehaviour
{

    #region Fields

    [SerializeField]
    protected float _nextAbilityTime;

    public event Action<float> OnAbilityTimeEvent;

    #endregion

    #region Properties

    public float NextAbilityTime => _nextAbilityTime;

    #endregion

    #region Methods

    protected virtual void OnAbilityTimeChanged(float time)
    {
        OnAbilityTimeEvent?.Invoke(time);
    }

    public abstract void SetUpAbility();

    public abstract void AbilityHandle();

    #endregion
}
