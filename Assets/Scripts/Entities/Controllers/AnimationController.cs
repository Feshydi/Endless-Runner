using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private Animator _playerAnimator;

    [SerializeField]
    private Animator _weaponAnimator;

    #endregion

    #region

    public void PlayIdleAnimation()
    {
        _playerAnimator.SetFloat("Speed", 0f);
        _weaponAnimator.SetFloat("Speed", 0f);
    }

    public void PlayMoveAnimation(float magnitude)
    {
        _playerAnimator.SetFloat("Speed", magnitude);
        _weaponAnimator.SetFloat("Speed", magnitude);
    }

    public void PlayRollAnimation()
    {
        _playerAnimator.SetTrigger("Roll");
        _weaponAnimator.SetTrigger("Roll");
    }

    public void PlayHitAnimation(float health)
    {
        _playerAnimator.SetFloat("Health", health);
        _weaponAnimator.SetFloat("Health", health);

        _playerAnimator.SetTrigger("Hit");
        _weaponAnimator.SetTrigger("Hit");
    }

    #endregion

}
