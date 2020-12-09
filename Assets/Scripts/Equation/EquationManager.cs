using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EquationManager : MonoBehaviour
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
        LevelManager.Instance.equation = ConvertNumber(LevelManager.Instance.currentNumber);
    }

    // Start is called before the first frame update
    void Start()
    {
        title.text = title.text + "\n" + LevelManager.Instance.equation;
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
                LevelManager.Instance.numberOfWordsNumbers = 2;
                SceneManager.LoadScene("Equation");
                LevelManager.Instance.level++;
                break;
            case (2):
                LevelManager.Instance.numberOfWordsNumbers = 4;
                SceneManager.LoadScene("Equation");
                LevelManager.Instance.level++;
                break;
            case (3):
                LevelManager.Instance.numberOfWordsNumbers = 4;
                SceneManager.LoadScene("Equation");
                LevelManager.Instance.level++;
                break;
            case (4):
                LevelManager.Instance.numberOfWordsNumbers = 4;
                SceneManager.LoadScene("Equation");
                LevelManager.Instance.level++;
                break;
            case (5):
                LevelManager.Instance.numberOfWordsNumbers = 6;
                SceneManager.LoadScene("Equation");
                LevelManager.Instance.level++;
                break;
            case (6):
                LevelManager.Instance.numberOfWordsNumbers = 6;
                SceneManager.LoadScene("Equation");
                LevelManager.Instance.level++;
                break;
            case (7):
                LevelManager.Instance.numberOfWordsNumbers = 6;
                SceneManager.LoadScene("Equation");
                LevelManager.Instance.level++;
                break;
            case (8):
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
    private string ConvertNumber(int number) 
    {

        if(number.Equals(0))
        {
            string[] equations0 = new string[9] { "6-6", "2-2", "1-1", "3-3", "4-4", "7-7", "5-5", "9-9", "8-8" };
            var postition = Random.Range(0, equations0.Length);
            return equations0[postition].ToString();
        }
        else if(number.Equals(1))
        {
            string[] equations1 = new string[8] { "10-9","2-1","8-7","5-4","4-3","6-5","7-6","9-8"};
            var postition = Random.Range(0, equations1.Length);
            return equations1[postition].ToString();
        }
        else if (number.Equals(2))
        {
            string[] equations2 = new string[5] { "8-6", "5-3", "10-8", "3-1", "4-2" };
            var postition = Random.Range(0, equations2.Length);
            return equations2[postition].ToString();
        }
        else if (number.Equals(3))
        {
            string[] equations3 = new string[8] { "5-2", "2+1", "4-1", "7-4", "6-3", "8-5", "1+2", "9-6" };
            var postition = Random.Range(0, equations3.Length);
            return equations3[postition].ToString();
        }
        else if (number.Equals(4))
        {
            string[] equations4 = new string[8] { "8-4", "7-3", "6-2", "9-5", "10-6", "2+2", "5-1", "1+3" };
            var postition = Random.Range(0, equations4.Length);
            return equations4[postition].ToString();
        }
        else if (number.Equals(5))
        {
            string[] equations5 = new string[7] { "1+4", "6-1", "4+1", "7-2", "9-4", "8-3", "2+3" };
            var postition = Random.Range(0, equations5.Length);
            return equations5[postition].ToString();
        }
        else if (number.Equals(6))
        {
            string[] equations6 = new string[9] { "8-2", "9-3", "5+1", "1+5", "2+4", "4+2", "7-1", "5+1", "3+3" };
            var postition = Random.Range(0, equations6.Length);
            return equations6[postition].ToString();
        }
        else if (number.Equals(7))
        {
            string[] equations7 = new string[7] { "9-2", "8-1", "10-3", "6+1", "3+4", "1+6", "5+2" };
            var postition = Random.Range(0, equations7.Length);
            return equations7[postition].ToString();
        }
        else if (number.Equals(8))
        {
            string[] equations8 = new string[6] { "2+6", "6+2", "10-2", "9-1", "1+7", "4+4" };
            var postition = Random.Range(0, equations8.Length);
            return equations8[postition].ToString();
        }
        else if (number.Equals(9))
        {
            string[] equations9 = new string[7] { "6+3", "3+6", "10-1", "2+7", "1+8", "8+1", "5+4" };
            var postition = Random.Range(0, equations9.Length);
            return equations9[postition].ToString();
        }
        else if (number.Equals(10))
        {
            string[] equations10 = new string[8] { "7+3", "6+4", "4+6", "5+5", "1+9", "8+2", "3+7", "2+8" };
            var postition = Random.Range(0, equations10.Length);
            return equations10[postition].ToString();
        }
        return "Error";

    }
    
}
