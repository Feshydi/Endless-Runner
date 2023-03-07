using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectController : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private GameObject _abilityEffect;

    [SerializeField]
    private TrailRenderer _rollTrailEffect;

    [SerializeField]
    private List<ParticleSystem> _eyesEffect;

    #endregion

    #region Methods

    private void Start()
    {
        _abilityEffect.SetActive(false);

        _rollTrailEffect.gameObject.SetActive(true);
        _rollTrailEffect.emitting = false;

        foreach (var eye in _eyesEffect)
        {
            eye.gameObject.SetActive(true);
            eye.Stop();
        }
    }

    public void EnableAbilityEffect()
    {
        _abilityEffect.SetActive(true);
    }

    public void DisableAbilityEffect()
    {
        _abilityEffect.SetActive(false);
    }

    public void EnableRollTrailEffect()
    {
        _rollTrailEffect.emitting = true;
    }

    public void DisableRollTrailEffect()
    {
        _rollTrailEffect.emitting = false;
    }

    public void EnableEyesEffect()
    {
        foreach (var eye in _eyesEffect)
        {
            eye.Play();
        }
    }

    public void DisableEyesEffect()
    {
        foreach (var eye in _eyesEffect)
        {
            eye.Stop();
        }
    }

    #endregion

}
