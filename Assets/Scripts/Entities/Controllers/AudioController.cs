using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private PlayerControllerBehaviour _playerController;

    [Header("Audio Sources")]
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
        _shootSound.Play();
    }

    public void PlayHitSound()
    {
        _hitSound.Play();
    }

    public void PlayAbilitySound()
    {
        _abilitySound.Play();
    }

    public void PlayRollSound()
    {
        _rollSound.Play();
    }

    #endregion

}

