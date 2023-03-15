using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Modes/Float Gameplay Mode")]
public class FloatGameplayMode : GameplayMode<float>
{
    public override bool IsGoalCompleted()
    {
        return CurrentValue >= GoalValue;
    }
}
