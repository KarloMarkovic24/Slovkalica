using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public TextMeshProUGUI textbox;
    public Transform parent;
    public GameObject container;

    private int textboxNumber = 15;

    private void Awake()
    {
        DictionaryWriter.Instance.UpdateScores();

        var players = DictionaryWriter.Instance.players;
        textbox.text = players[0].name + " - " + players[0].score;
        
        for (int i = 1; i < players.Count; i++)
        {
            if (i >= textboxNumber)
            {
                container.GetComponent<RectTransform>().sizeDelta = new Vector2(container.GetComponent<RectTransform>().sizeDelta.x, container.GetComponent<RectTransform>().sizeDelta.y + 50);
            }
            var newTextbox = Instantiate(textbox, parent, true);
            newTextbox.text = players[i].name + " - " + players[i].score;
        }
    }
}
