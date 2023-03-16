using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;
using UnityEditor;
using UnityEngine.UI;

public class EnumSelector<T> : MonoBehaviour where T : Enum
{

    #region Fields

    public GameplayManager gameplayManager;
    public TMP_Dropdown dropdown;
    public event Action<T> onValueChanged;

    #endregion

    #region Methods

    private void OnEnable()
    {
        string selected = "";
        foreach (FieldInfo field in typeof(GameplayManager).GetFields())
        {
            if (field.FieldType == typeof(T))
            {
                selected = ((T)field.GetValue(gameplayManager)).ToString();
            }
        }

        dropdown.ClearOptions();

        string[] enumNames = System.Enum.GetNames(typeof(T));

        int index = 0;
        TMP_Dropdown.OptionData[] options = new TMP_Dropdown.OptionData[enumNames.Length];
        for (int i = 0; i < enumNames.Length; i++)
        {
            string prettyName = GetPrettyName(enumNames[i]);
            options[i] = new TMP_Dropdown.OptionData(prettyName);
            if (enumNames[i] == selected)
                index = i;
        }

        dropdown.AddOptions(new List<TMP_Dropdown.OptionData>(options));

        dropdown.value = index;
        onValueChanged += OnSelectorChanged;
    }

    private void OnDisable() => onValueChanged -= OnSelectorChanged;

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

    protected virtual void OnSelectorChanged(T value) { }

    #endregion

}
