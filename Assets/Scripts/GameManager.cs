using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool init, minusScore;
    private int score;
    private GameObject[] letters;
    
    public static bool click;
    public TextMeshProUGUI title;

    private void Awake()
    {
        click = false;
        score = 0;

        LevelManager.Instance.definedLetter = RandomLetter();
    }
    private void Start()
    {        
        title.text = title.text + " " + LevelManager.Instance.definedLetter;
    }

    private void Update()
    {
        if (!init)
        {
            letters = GameObject.FindGameObjectsWithTag("Letter");
            init = true;
        }

        if (click)
        {
            click = false;
            if (SceneManager.GetActiveScene().name.Equals("Letters"))
            {
                for (int i = 0; i < letters.Length; i++)
                {

                    if (letters[i].GetComponentInChildren<Hover>().Clicked && 
                        letters[i].GetComponent<TextMeshProUGUI>().text.ToUpper().Equals(LevelManager.Instance.definedLetter.ToUpper()))
                    {
                        letters[i].GetComponentInChildren<Hover>().Green = true;
                        letters[i].GetComponentInChildren<Hover>().Clicked = false;
                        letters[i].GetComponentInChildren<Hover>().Clickable = false;
                        LevelManager.Instance.score++;
                        ++score;
                        
                        if (score == LevelManager.Instance.numberOfDefinedLetters) StartCoroutine(ScoreChecker());
                    }
                    else if (letters[i].GetComponentInChildren<Hover>().Clicked && !letters[i].GetComponent<TextMeshProUGUI>().text.Equals(LevelManager.Instance.definedLetter))
                    {
                        letters[i].GetComponentInChildren<Hover>().Clicked = false;
                        letters[i].GetComponentInChildren<Hover>().Clickable = false;
                        letters[i].GetComponentInChildren<Hover>().Red = true;

                        LevelManager.Instance.score--;        
                    }
                }
            }
            else if (SceneManager.GetActiveScene().name.Equals("LetterToWord"))
            {
                for (int i = 0; i < letters.Length; i++)
                {
                    if (letters[i].GetComponentInChildren<Hover>().Clicked && 
                        ((letters[i].GetComponent<TextMeshProUGUI>().text[0].ToString().ToUpper() + "" + letters[i].GetComponent<TextMeshProUGUI>().text[1].ToString().ToUpper()).Equals(LevelManager.Instance.definedLetter.ToUpper())
                        || letters[i].GetComponent<TextMeshProUGUI>().text[0].ToString().ToUpper().Equals(LevelManager.Instance.definedLetter.ToUpper())))
                        /*&& CroatianSpeciality(letters[i].GetComponent<TextMeshProUGUI>().text.ToLower())*/
                    {
                        letters[i].GetComponentInChildren<Hover>().Clicked = false;
                        letters[i].GetComponentInChildren<Hover>().Clickable = false;
                        letters[i].GetComponentInChildren<Hover>().Green = true;
                        LevelManager.Instance.score++;
                        ++score;
                        if (score == LevelManager.Instance.numberOfDefinedWords) StartCoroutine(ScoreChecker());
                    }
                    else if (letters[i].GetComponentInChildren<Hover>().Clicked && !letters[i].GetComponent<TextMeshProUGUI>().text[0].Equals(LevelManager.Instance.definedLetter))
                    {
                        letters[i].GetComponentInChildren<Hover>().Clicked = false;
                        letters[i].GetComponentInChildren<Hover>().Clickable = false;
                        letters[i].GetComponentInChildren<Hover>().Red = true;

                        LevelManager.Instance.score--;          
                    }
                } 
            }            
        }
    }

    private bool CroatianSpeciality(string input)
    {
        if (input.Length == 2)
        {
            switch (input)
            {
                case "lj":
                    if (input[0].ToString().Equals("l") && input[1].ToString().Equals("j"))
                        return true;
                    return false;
                case "nj":
                    if (input[0].ToString().Equals("n") && input[1].ToString().Equals("j"))
                        return true;
                    return false;
                case "dž":
                    if (input[0].ToString().Equals("d") && input[1].ToString().Equals("ž"))
                        return true;
                    return false;
            }
        }
        else if (input.Length > 2)
        {
            switch (input[0].ToString())
            {
                case "l":
                    if (input[1].ToString().Equals("j"))
                        return true;
                    else if (!input[1].ToString().Equals("j"))
                        return true;
                    return false;
                case "n":
                    if (input[1].ToString().Equals("j"))
                        return true;
                    else if (!input[1].ToString().Equals("j"))
                        return true;
                    return false;
                case "d":
                    if (input[1].ToString().Equals("ž"))
                        return true;
                    else if (!input[1].ToString().Equals("ž"))
                        return true;
                    return false;
                default:
                    return true;
            }
        }

        return false;
    }

    private string RandomLetter()
    {
        var randomNumber = Random.Range(0, 29);

        return DictionaryReader.Instance.dictionary.ElementAt(randomNumber).Key;
    }

    private IEnumerator ScoreChecker()
    {
        yield return new WaitForSeconds(2);

        if (SceneManager.GetActiveScene().name.Equals("Letters"))
        {
            switch (LevelManager.Instance.level)
            {
                case (1):
                    LevelManager.Instance.numberOfLetters = 6;
                    SceneManager.LoadScene("Letters");
                    LevelManager.Instance.level++;
                    break;
                case (2):
                    LevelManager.Instance.numberOfLetters = 9;
                    if (PlayerPrefs.GetInt("LevelLength", 0) == 0) 
                        LetterLevel();
                    else
                    {
                        LevelManager.Instance.level++;
                        SceneManager.LoadScene("Letters");
                    }
                    break;
                case (3):
                    LevelManager.Instance.numberOfLetters = 12;
                    SceneManager.LoadScene("Letters");
                    LevelManager.Instance.level++;
                    break;
                case (4):
                    LevelManager.Instance.numberOfLetters = 18;
                    SceneManager.LoadScene("Letters");
                    LevelManager.Instance.level++;
                    break;
                case (5):
                    LetterLevel();
                    break;
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("LetterToWord"))
        {
            switch (LevelManager.Instance.level)
            {
                case (1):
                    LevelManager.Instance.numberOfWords = 4;
                    SceneManager.LoadScene("LetterToWord");
                    LevelManager.Instance.level++;
                    break;
                case (2):
                    LevelManager.Instance.numberOfWords = 6;
                    if (PlayerPrefs.GetInt("LevelLength", 0) == 0) 
                        WordLevel();
                    else
                    {
                        LevelManager.Instance.level++;
                        SceneManager.LoadScene("LetterToWord");
                    }
                    break;
                case (3):
                    LevelManager.Instance.numberOfWords = 9;
                    SceneManager.LoadScene("LetterToWord");
                    LevelManager.Instance.level++;
                    break;
                case (4):
                    WordLevel();
                    break;
            }            
        }
    }

    private void LetterLevel()
    {
        LevelManager.Instance.level = 1;
        if (!LevelManager.Instance.upper)
        {
            LevelManager.Instance.upper = true;
            LevelManager.Instance.numberOfLetters = 4;
            SceneManager.LoadScene("Letters");
        }
        else if (!LevelManager.Instance.lowerUpper)
        {
            LevelManager.Instance.lowerUpper = true;
            LevelManager.Instance.numberOfLetters = 4;
            SceneManager.LoadScene("Letters");
        }
        else
        {
            LevelManager.Instance.upper = false;
            LevelManager.Instance.lowerUpper = false;
            if (!LevelManager.Instance.playerName.ToString().Equals(""))
                LevelManager.Instance.WritePlayerScore();
            SceneManager.LoadScene("Letter Game Menu");
        }
    }

    private void WordLevel()
    {
        LevelManager.Instance.level = 1;
        if (!LevelManager.Instance.upper)
        {
            LevelManager.Instance.upper = true;
            LevelManager.Instance.numberOfLetters = 2;
            SceneManager.LoadScene("LetterToWord");
        }
        else if (!LevelManager.Instance.lowerUpper)
        {
            LevelManager.Instance.lowerUpper = true;
            LevelManager.Instance.numberOfLetters = 2;
            SceneManager.LoadScene("LetterToWord");
        }
        else
        {
            LevelManager.Instance.upper = false;
            LevelManager.Instance.lowerUpper = false;
            if (!LevelManager.Instance.playerName.ToString().Equals(""))
                LevelManager.Instance.WritePlayerScore();
            SceneManager.LoadScene("Letter Game Menu");
        }
    }
}
