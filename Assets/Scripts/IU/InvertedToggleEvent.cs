using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvertedToggleEvent : MonoBehaviour
{
    [System.Serializable]
    public class UnityEventBool : UnityEngine.Events.UnityEvent<bool> { }
    public UnityEventBool onValueChangedInverse;

    private void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener((on) => { onValueChangedInverse.Invoke(!on); });
    }
}