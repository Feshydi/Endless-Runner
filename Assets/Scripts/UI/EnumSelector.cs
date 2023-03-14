using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnumSelector<T> : MonoBehaviour
{

    #region Fields

    public TMP_Dropdown dropdown;
    public event Action<T> onValueChanged;

    #endregion

    #region Methods

    private void Start()
    {
        dropdown.ClearOptions();

        string[] enumNames = System.Enum.GetNames(typeof(T));

        TMP_Dropdown.OptionData[] options = new TMP_Dropdown.OptionData[enumNames.Length];
        for (int i = 0; i < enumNames.Length; i++)
        {
            string prettyName = GetPrettyName(enumNames[i]);
            options[i] = new TMP_Dropdown.OptionData(prettyName);
        }

        dropdown.AddOptions(new List<TMP_Dropdown.OptionData>(options));

        OnValueChanged(0);
    }

    public void OnValueChanged(int index)
    {
        T selector = ParsePrettyName(dropdown.options[index].text);
        onValueChanged?.Invoke(selector);
    }

    private string GetPrettyName(string name)
    {
        string prettyName = "";
        for (int i = 0; i < name.Length; i++)
        {
            if (i > 0 && char.IsUpper(name[i]))
                prettyName += " ";
            prettyName += name[i];
        }
        return prettyName;
    }

    private T ParsePrettyName(string prettyName)
    {
        string name = "";
        for (int i = 0; i < prettyName.Length; i++)
        {
            if (prettyName[i] != ' ')
                name += prettyName[i];
        }
        return (T)Enum.Parse(typeof(T), name);
    }

    #endregion

}
