using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player")]
public class CharacterData : EntityData
{

    #region Fields

    [SerializeField]
    private float _projectileSpeed;

    #endregion

    #region Properties

    public float ProjectileSpeed => _projectileSpeed;

    #endregion

}
