using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImpulseEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    #region Fields

    [SerializeField]
    private float _duration;

    [SerializeField]
    private float _scale;

    private LTSeq _sequence;

    #endregion

    #region Methods

    private void OnEnable()
    {
        StartAnimation();
    }

    private void OnDisable()
    {
        StopAnimation();
    }

    public void StartAnimation()
    {
        _sequence = LeanTween.sequence();
        _sequence.append(LeanTween.scale(gameObject, Vector3.one * _scale, _duration).setEase(LeanTweenType.easeOutQuad).setLoopPingPong());
    }

    public void StopAnimation()
    {
        LeanTween.cancel(gameObject);
        _sequence = null;
        LeanTween.scale(gameObject, Vector3.one, 0.2f).setEase(LeanTweenType.easeInOutQuad);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAnimation();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartAnimation();
    }

    #endregion

}
