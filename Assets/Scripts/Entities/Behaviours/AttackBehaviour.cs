using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{

    #region Fields

    [Header("General")]
    [SerializeField]
    private Gun _gun;

    #endregion

    #region Methods

    public void ShootHandle()
    {
        _gun.Shoot();
    }

    #endregion

}
