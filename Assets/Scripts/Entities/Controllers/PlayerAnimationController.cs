using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
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
        _playerAnimator.SetBool("Idle", true);
        _weaponAnimator.SetBool("Idle", true);
    }

    public void PlayMoveAnimation()
    {
        _playerAnimator.SetBool("Idle", false);
        _weaponAnimator.SetBool("Idle", false);
    }

    public void PlayRollAnimation()
    {
        _playerAnimator.SetTrigger("Roll");
        _weaponAnimator.SetTrigger("Roll");
    }

    /// <summary>
    /// if health <= 0, after play death animation
    /// </summary>
    /// <param name="health"></param>
    public void PlayHitAnimation(float health)
    {
        _playerAnimator.SetFloat("Health", health);
        _weaponAnimator.SetFloat("Health", health);

        _playerAnimator.SetTrigger("Hit");
        _weaponAnimator.SetTrigger("Hit");
    }

    #endregion

}
