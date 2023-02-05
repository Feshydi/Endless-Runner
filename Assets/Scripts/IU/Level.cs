using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Level : MonoBehaviour
{

    #region Fields

    [SerializeField]
    private LevelSelection _levelSelection;

    [SerializeField]
    private LevelData _levelData;

    [SerializeField]
    private Button _thisButton;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private TextMeshProUGUI _name;

    #endregion

    #region Methods

    private void Awake()
    {
        if (_thisButton == null) _thisButton = GetComponent<Button>();
        if (_image == null) _image = GetComponent<Image>();
        if (_name == null) _name = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Init(LevelSelection parent, LevelData levelData)
    {
        _levelSelection = parent;
        _levelData = levelData;
        _image.sprite = levelData.Icon;
        _name.text = levelData.Name;
    }

    public void OnClick()
    {
        _levelSelection.SelectLevel(_thisButton, _levelData);
    }

    #endregion

}
