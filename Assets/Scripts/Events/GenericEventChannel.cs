using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericEventChannel<T> : ScriptableObject
{
    public event Action<T> OnEventRaised;

    public void RaiseEvent(T value)
    {
        OnEventRaised?.Invoke(value);
    }
}
