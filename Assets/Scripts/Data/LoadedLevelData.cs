using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/LoadedLevelData")]
public class LoadedLevelData : ScriptableObject
{

    #region Fields

    public PlayerControllerBehaviour Player;

    public LevelData LevelData;

    public bool AutoSeedGeneration;

    public int Seed;

    #endregion

}
