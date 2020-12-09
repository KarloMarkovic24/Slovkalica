using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NumberManager : MonoBehaviour
{
    private bool init;
    private GameObject[] numbers;
    private string[] numbersToWords = new string[11] { "nula", "jedan", "dva", "tri", "četri", "pet", "šest", "sedam", "osam", "devet", "deset" };
  
    public static bool click;
    public TextMeshProUGUI title;

    private void Awake()
    {
        click = false;

        LevelManager.Instance.currentNumber = RandomNumber();
    }

    // Start is called before the first frame update
    void Start()
    {
        title.text = title.text + " " + LevelManager.Instance.currentNumber;
    }
    //array s brojem u indeksu!!
    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            numbers = GameObject.FindGameObjectsWithTag("NumbersTag");
            init = true;
        }
        if (click)
        {
            
            click = false;
            for (int i = 0; i < numbers.Length; i++)
            {

                if (numbers[i].GetComponentInChildren<Hover>().Clicked &&
                    ((numbers[i].GetComponent<TextMeshProUGUI>().text.ToString().Equals(numbersToWords[LevelManager.Instance.currentNumber].ToString()))))
                /*&& CroatianSpeciality(letters[i].GetComponent<TextMeshProUGUI>().text.ToLower())*/
                {
                    
                    numbers[i].GetComponentInChildren<Hover>().Clicked = false;
                    numbers[i].GetComponentInChildren<Hover>().Clickable = false;
                    numbers[i].GetComponentInChildren<Hover>().Green = true;
                    LevelManager.Instance.score++;

                    StartCoroutine(ScoreChecker());
                }
                else if (numbers[i].GetComponentInChildren<Hover>().Clicked && ((!numbers[i].GetComponent<TextMeshProUGUI>().text.ToString().Equals(numbersToWords[LevelManager.Instance.currentNumber]))))
                {
                    numbers[i].GetComponentInChildren<Hover>().Clicked = false;
                    numbers[i].GetComponentInChildren<Hover>().Clickable = false;
                    numbers[i].GetComponentInChildren<Hover>().Red = true;

                    LevelManager.Instance.score--;
                }
            }
        }



    }

    private int RandomNumber()
    {
        var randomNumber = 0;
        while (true)
        {
        randomNumber = Random.Range(0, 11);
        if (LevelManager.Instance.number[randomNumber].Equals(0))
            {
                LevelManager.Instance.number[randomNumber] = 1;
                break;
            }
        }
        
        return randomNumber;
    }

    private IEnumerator ScoreChecker()
    {
        yield return new WaitForSeconds(2);

        
        switch (LevelManager.Instance.level)
        {
            case (1):
                LevelManager.Instance.numberOfWordsNumbers = 4;
                SceneManager.LoadScene("Numbers");
                LevelManager.Instance.level++;  
                break;
            case (2):
                LevelManager.Instance.numberOfWordsNumbers = 4;
                SceneManager.LoadScene("Numbers");
                LevelManager.Instance.level++;
                break;
            case (3):
                LevelManager.Instance.numberOfWordsNumbers = 4;
                SceneManager.LoadScene("Numbers");
                LevelManager.Instance.level++;
                break;
            case (4):
                LevelManager.Instance.numberOfWordsNumbers = 6;
                SceneManager.LoadScene("Numbers");
                LevelManager.Instance.level++;
                break;
            case (5):
                LevelManager.Instance.numberOfWordsNumbers = 6;
                SceneManager.LoadScene("Numbers");
                LevelManager.Instance.level++;
                break;
            case (6):
                LevelManager.Instance.numberOfWordsNumbers = 6;
                SceneManager.LoadScene("Numbers");
                LevelManager.Instance.level++;
                break;
            case (7):
                EndGame();
                break;
        }
        
    }
    private void EndGame()
    {
        for (int i = 0; i < LevelManager.Instance.number.Length; i++) LevelManager.Instance.number[i] = 0;
       
        if (!LevelManager.Instance.playerName.ToString().Equals(""))
        LevelManager.Instance.WritePlayerScore();
        LevelManager.Instance.score = 0;
        SceneManager.LoadScene("Letter Game Menu");
 
    }
}
