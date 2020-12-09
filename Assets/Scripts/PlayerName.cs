using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    public void WritePlayerName()
    {
        LevelManager.Instance.playerName = gameObject.GetComponent<TMP_InputField>().text;
    }
}
