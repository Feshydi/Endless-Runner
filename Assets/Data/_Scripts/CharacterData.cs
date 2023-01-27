using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Player")]
public class CharacterData : ScriptableObject
{

    #region Fields

    [SerializeField]
    private float _moveSpeed;



    #endregion

    #region Properties

    public float MoveSpeed => _moveSpeed;

    #endregion

}
