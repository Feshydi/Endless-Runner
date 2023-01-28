using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private PlayerController _player;

    #endregion

    #region Methods

    public void DoAfterRoll()
    {
        _player.AfterAnimation();
    }

    #endregion

}
