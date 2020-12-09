using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown levelDropdown, secondsDropdown;

    private void Awake()
    {
        secondsDropdown.value = PlayerPrefs.GetInt("HoverTime", 2) - 1;
        var levelLength = PlayerPrefs.GetInt("LevelLength", 1);
        if (levelLength == 0 && levelDropdown.value == 1)
            levelDropdown.value = 0;
        else if (levelLength == 1 && levelDropdown.value == 0)
            levelDropdown.value = 1;
    }

    public void SecondsDropdownChange()
    {
        PlayerPrefs.SetInt("HoverTime", secondsDropdown.value + 1);
        Debug.Log(secondsDropdown.value);
    }

    public void LevelDropdownChange()
    {
        PlayerPrefs.SetInt("LevelLength", levelDropdown.value);
    }
}
