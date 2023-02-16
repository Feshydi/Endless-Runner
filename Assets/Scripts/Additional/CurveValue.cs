using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveValue
{

    #region Fields

    [SerializeField]
    private AnimationCurve _curve;

    [SerializeField]
    private float _minValue;

    [SerializeField]
    private float _maxValue;

    [SerializeField]
    private float _maxTime;

    #endregion

    #region Methods

    public void Init(AnimationCurve curve, float minValue, float maxValue, float maxMinTime)
    {
        _curve = curve;
        _minValue = minValue;
        _maxValue = maxValue;
        _maxTime = maxMinTime * 60;
    }

    public float GetValue(float t)
    {
        var range = _maxValue - _minValue;
        return _minValue + GetCurve(t) * range;
    }

    public float GetCurve(float t)
    {
        return _curve.Evaluate(t < _maxTime ? t / _maxTime : 1);
    }

    #endregion

}