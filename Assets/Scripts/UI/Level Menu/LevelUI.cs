using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : MonoBehaviour
{

    #region Fields

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _name;

    #endregion

    #region Methods

    public void Init(LevelData levelData)
    {
        _image.sprite = levelData.Icon;
        _name.text = levelData.Name;
    }

    #endregion

}
