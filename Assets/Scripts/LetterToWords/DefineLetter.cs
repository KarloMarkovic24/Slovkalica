using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefineLetter : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = LevelManager.Instance.definedLetter;
    }
}
