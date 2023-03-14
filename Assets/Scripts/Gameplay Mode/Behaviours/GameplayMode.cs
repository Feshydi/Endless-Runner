using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayMode<T> : ScriptableObject, IGoalable
{

    #region Fields

    [SerializeField] protected string Id;
    [SerializeField] protected string Description;
    [SerializeField] protected T GoalValue;
    [SerializeField] protected T CurrentValue;

    public bool IsEnabled;
    public GenericEventChannel<T> GameplayEventChannel;

    #endregion

    #region Methods

    public void Init()
    {
        GameplayEventChannel.OnEventRaised -= UpdateCurrentValue;

        if (IsEnabled)
            GameplayEventChannel.OnEventRaised += UpdateCurrentValue;
    }

    public void SetGoalValue(T value) => GoalValue = value;

    public void UpdateCurrentValue(T value)
    {
        CurrentValue = value;
        IsGoalCompleted();
    }

    public abstract bool IsGoalCompleted();

    #endregion

}
