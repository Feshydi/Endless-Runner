using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mode
{
    Survival,
    Scoring,
    Timer
}

public abstract class GameplayMode<T> : ScriptableObject, IGoalable
{

    #region Fields

    [SerializeField] protected T GoalValue;
    [SerializeField] protected T CurrentValue;

    public Mode Mode;
    public bool IsEnabled;
    public GenericEventChannel<T> GameplayEventChannel;
    public VoidEventChannel GameCompletedEventChannel;

    [TextArea(3, 6)]
    public string Description;

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
        if (IsGoalCompleted())
            GameCompletedEventChannel?.RaiseEvent();
    }

    public abstract bool IsGoalCompleted();

    #endregion

}
