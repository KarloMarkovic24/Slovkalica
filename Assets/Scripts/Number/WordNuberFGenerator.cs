using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordNuberFGenerator : MonoBehaviour
{
    public GameObject wordNumber;
    public Transform parentOfWordNumber;
    private string[] numbersToWords = new string[11] { "nula", "jedan", "dva", "tri", "četri", "pet", "šest", "sedam", "osam", "devet", "deset" };

    // Start is called before the first frame update
    private void Awake()
    {
        if (LevelManager.Instance.numberOfWordsNumbers <= 6)
        {
            
            gameObject.GetComponent<GridLayoutGroup>().constraintCount = LevelManager.Instance.numberOfWordsNumbers / 2;
        }
        else
            gameObject.GetComponent<GridLayoutGroup>().constraintCount = LevelManager.Instance.numberOfWordsNumbers / 3;
    }
    private void Start()
    {
       
        GenerateWordsNumber();
    }

    private void GenerateWordsNumber()
    {
        bool minimumWord = false;
        var randomWordNumber = RandomWordNumber();
        var corect = Random.Range(1, LevelManager.Instance.numberOfWordsNumbers);
        Debug.Log("Correct je:" + corect);
        
        wordNumber.GetComponent<TextMeshProUGUI>().text = randomWordNumber;

        if (randomWordNumber.ToString().Equals(numbersToWords[LevelManager.Instance.currentNumber]))
        {
            minimumWord = true;
           
        }
        for (int i = 1; i < LevelManager.Instance.numberOfWordsNumbers; i++)
        {
            var newWord = Instantiate(wordNumber, parentOfWordNumber, true);

            if (i == corect && !minimumWord)
            {
                minimumWord = true;
                LevelManager.Instance.selectedWord[LevelManager.Instance.currentNumber] = 1;
                var wordNumber = numbersToWords[LevelManager.Instance.currentNumber];
                newWord.GetComponent<TextMeshProUGUI>().text = wordNumber;
                
            }
            else
            {
                randomWordNumber = RandomWordNumber();
                newWord.GetComponent<TextMeshProUGUI>().text = randomWordNumber;
                if (randomWordNumber.ToString().Equals(numbersToWords[LevelManager.Instance.currentNumber]))
                {
                    minimumWord = true;
                }
            }
        }
        for (int i = 0; i < LevelManager.Instance.number.Length; i++) LevelManager.Instance.selectedWord[i] = 0;
    }
    private string RandomWordNumber()
    {
        var randomNumber = 0;
        while (true)
        {
            randomNumber = Random.Range(0, 11);
            
            if (LevelManager.Instance.selectedWord[randomNumber].Equals(0))
            {
                LevelManager.Instance.selectedWord[randomNumber] = 1;
                break;
            }
        }
        string selectedWord = numbersToWords[randomNumber];
     
        return selectedWord;
    }
}
