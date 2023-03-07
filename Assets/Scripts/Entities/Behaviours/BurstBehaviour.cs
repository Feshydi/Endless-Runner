using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstBehaviour : MonoBehaviour
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private CharacterData _characterData;

    [Header("Generated")]
    [SerializeField]
    private float _burstTime;

    [SerializeField]
    private float _cooldownTime;

    [SerializeField]
    private bool _isBurst;

    [SerializeField]
    private float _nextBurstTime;

    public event Action<float> OnBurstTimeEvent;

    #endregion

    #region Properties

    public bool IsBurst => _isBurst;

    public float NextBurstTime => _nextBurstTime;

    #endregion

    #region Methods

    private void Start()
    {
        _burstTime = _characterData.BurstTime;
        _cooldownTime = _characterData.BurstCooldownTime;
    }

    public void BurstHandle(PlayerEffectController effectController)
    {
        _nextBurstTime = Time.time + _cooldownTime;
        _isBurst = true;
        effectController.EnableEyesEffect();
        OnBurstTimeEvent?.Invoke(_cooldownTime);
        StartCoroutine(BurstTimer(effectController));
    }

    private IEnumerator BurstTimer(PlayerEffectController effectController)
    {
        yield return new WaitForSeconds(_burstTime);
        _isBurst = false;
        effectController.DisableEyesEffect();
    }

    #endregion

}
