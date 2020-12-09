using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpellingImageManager : MonoBehaviour
{
    public Sprite[] images;
    public Image image;
    public GameObject fieldElement, letter;
    public Transform fieldElementParent, letterParent;

    public static bool Clicked;
    public static string InputChar;

    private int imageChars, score, objective, fields, doubleCharPosition;
    private string imageName;
    private Dictionary<string, int> missingLetters;
    private List<string> missingChars, missingToImage;
    private List<TextMeshProUGUI> underscoreChars;
    private void Awake()
    {
        var randomNumber = UnityEngine.Random.Range(0, images.Length);
        image.sprite = images[randomNumber];
        imageName = image.sprite.name;
        imageChars = imageName.Length;
        fields = 0;
        score = 0;
        doubleCharPosition = -1;

        missingLetters = new Dictionary<string, int>();
        missingChars = new List<string>();
        underscoreChars = new List<TextMeshProUGUI>();

        Clicked = false;
        InputChar = "";

        FieldGenerator();
        LetterGenerator();

        missingToImage = new List<string>();
        for (int i = 0; i < missingLetters.Count; i++)        
            missingToImage.Add(missingLetters.ElementAt(i).Key.ToString());
    }

    private void Update()
    {
        if (Clicked)
        {
            LetterChecker();
            if (score == objective) StartCoroutine(ScoreChecker());
        }
    }

    private void LetterChecker()
    {
        Clicked = false;
        if (missingToImage.Contains(InputChar))
        {
            for (int j = fields; j < underscoreChars.Count; j++)
            {
                if (!underscoreChars[j].text.Equals(" ")) continue;
                for (int i = 0; i < missingToImage.Count; i++)
                {
                    if (missingToImage[i].Equals(InputChar) && missingLetters[InputChar] == j && underscoreChars[j].text == " ")
                    {
                        underscoreChars[j].text = InputChar;
                        ++score;
                        LevelManager.Instance.score++;
                        //++fields;
                        missingToImage.Remove(InputChar);
                        InputChar = null;
                        return; 
                    }
                }
            }
        }
    }

    private void FieldGenerator()
    {
        for (int i = 0; i < imageChars; i++)
        {
            var randomTrigger = UnityEngine.Random.Range(0, 2) == 1;
            if (randomTrigger && missingLetters.Count < LevelManager.Instance.level && !missingChars.Contains(imageName[i].ToString()))
            {
                var tmpChar = imageName[i].ToString();
                imageName = ReplaceFirst(imageName, imageName[i].ToString(), " ");
                Debug.Log("stari " + i + " novi " + doubleCharPosition);
                if (doubleCharPosition != -1)
                {
                    missingLetters.Add(tmpChar, doubleCharPosition);
                    missingChars.Add(tmpChar);
                    doubleCharPosition = -1;
                }
                else
                {
                    missingLetters.Add(tmpChar, i);
                    missingChars.Add(tmpChar);
                }

                ++objective;
            }
            else if (i == imageChars - 1 && missingLetters.Count == 0)
            {
                missingLetters.Add(imageName[i].ToString(), i);
                missingChars.Add(imageName[i].ToString());
                imageName = ReplaceFirst(imageName, imageName[i].ToString(), " ");
                ++objective;
            }
        }

        fieldElement.GetComponentInChildren<TextMeshProUGUI>().text = imageName[0].ToString();
        underscoreChars.Add(fieldElement.GetComponentInChildren<TextMeshProUGUI>());
        for (int i = 1; i < imageChars; i++)
        {
            var newElement = Instantiate(fieldElement, fieldElementParent, true).GetComponentInChildren<TextMeshProUGUI>();
            newElement.text = imageName[i].ToString();
            underscoreChars.Add(newElement);
        }
    }

    private string ReplaceFirst(string text, string search, string replace)
    {
        int pos = text.IndexOf(search, StringComparison.Ordinal);
        if (pos < 0)
        {
            return text;
        }

        doubleCharPosition = pos;
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }

    private void LetterGenerator()
    {
        for (int i = 0; i < LevelManager.Instance.level * 2 - missingLetters.Count; i++)
        {
            var randomChar = DictionaryReader.Instance.dictionary.ElementAt(UnityEngine.Random.Range(0, 30)).Key;
            if (missingChars.Contains(randomChar))
            {
                --i;
                continue;
            }
            missingChars.Add(randomChar);            
        }
        var shuffled = missingChars.OrderBy(x => Guid.NewGuid()).ToList();
        missingChars = shuffled;

        letter.GetComponent<TextMeshProUGUI>().text = missingChars[0];
        for (int i = 1; i < missingChars.Count; i++)
        {
            var newLetter = Instantiate(letter, letterParent, true).GetComponent<TextMeshProUGUI>();
            newLetter.text = missingChars[i];
        }
    }

    private IEnumerator ScoreChecker()
    {
        yield return new WaitForSeconds(2);       
           
        if ((LevelManager.Instance.level > 2 && PlayerPrefs.GetInt("LevelLength", 0) == 0) || 
            (PlayerPrefs.GetInt("LevelLength", 0) == 1 && LevelManager.Instance.level > 3))
        {
            LevelManager.Instance.level = 1;
            if (!LevelManager.Instance.playerName.ToString().Equals(""))
                LevelManager.Instance.WritePlayerScore();
            SceneManager.LoadScene("Letter Game Menu");
        }
        else
        {
            LevelManager.Instance.level++;
            SceneManager.LoadScene("SpellingImage");
        }
    }
}