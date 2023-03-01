using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private AudioSource _moveSound;

    [SerializeField]
    private AudioSource _shootSound;

    [SerializeField]
    private AudioSource _hitSound;

    [SerializeField]
    private AudioSource _abilitySound;

    [SerializeField]
    private AudioSource _rollSound;

    [Header("Settings")]
    [SerializeField]
    private float _pitchRange;

    #endregion

    #region Methods

    public void EnableMoveSound()
    {
        _moveSound?.Play();
    }

    public void DisableMoveSound()
    {
        _moveSound?.Stop();
    }

    public void PlayShootSound()
    {
        _shootSound.pitch = Random.Range(1 - _pitchRange, 1 + _pitchRange);
        _shootSound.Play();
    }

    public void PlayHitSound()
    {
        _hitSound.pitch = Random.Range(1 - _pitchRange, 1 + _pitchRange);
        _hitSound.Play();
    }

    public void PlayAbilitySound()
    {
        _abilitySound.Play();
    }

    public void PlayRollSound()
    {
        _rollSound.pitch = Random.Range(1 - _pitchRange, 1 + _pitchRange);
        _rollSound.Play();
    }

    #endregion

}

