using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterFieldGenerator : MonoBehaviour
{
    public GameObject letter;
    public Transform parentOfLetter;
    private int numberOfDefinedLetters;
    private bool lowerUpper;
    
    private void Awake()
    {

        if (LevelManager.Instance.numberOfLetters <= 6)
            gameObject.GetComponent<GridLayoutGroup>().constraintCount = 2;    
        else if (LevelManager.Instance.numberOfLetters < 16)
            gameObject.GetComponent<GridLayoutGroup>().constraintCount = 4;
        else 
            gameObject.GetComponent<GridLayoutGroup>().constraintCount = 6;
    }
    private void Start()
    {
        GenerateLetters();
    }

    private void GenerateLetters()
    {
        bool minimumLetter = false;
        var randomLetter = RandomLetter();
        var corectL = Random.Range(1, LevelManager.Instance.numberOfLetters);

        if (LevelManager.Instance.upper && !LevelManager.Instance.lowerUpper)
            letter.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.UpperCase;
        else if (LevelManager.Instance.upper && LevelManager.Instance.lowerUpper)
        {
            lowerUpper = true;
            if (Random.Range(0, 2) == 1 ? true : false) randomLetter = randomLetter.ToUpper();
        }      

        letter.GetComponent<TextMeshProUGUI>().text = randomLetter;
        if (randomLetter.ToUpper().Equals(LevelManager.Instance.definedLetter.ToUpper()))
        {
            minimumLetter = true;
            ++numberOfDefinedLetters;
            LevelManager.Instance.numberOfDefinedLetters = numberOfDefinedLetters;
        }
        for (int i = 1; i < LevelManager.Instance.numberOfLetters; i++)
        {
            var newLetter = Instantiate(letter, parentOfLetter, true);

            if (i == corectL && !minimumLetter)
            {
                newLetter.GetComponent<TextMeshProUGUI>().text = LevelManager.Instance.definedLetter;
                ++numberOfDefinedLetters;
                LevelManager.Instance.numberOfDefinedLetters = numberOfDefinedLetters;
            }
            else
            {
                randomLetter = RandomLetter();

                if (lowerUpper)
                    if (Random.Range(0, 2) == 1 ? true : false) randomLetter = randomLetter.ToUpper();

                newLetter.GetComponent<TextMeshProUGUI>().text = randomLetter;
                
                if (randomLetter.ToUpper().Equals(LevelManager.Instance.definedLetter.ToUpper()))
                {
                    minimumLetter = true;
                    ++numberOfDefinedLetters;
                    LevelManager.Instance.numberOfDefinedLetters = numberOfDefinedLetters;
                }
            }
        }
    }

    private string RandomLetter()
    {
        var randomNumber = Random.Range(0, 29);

        return DictionaryReader.Instance.dictionary.ElementAt(randomNumber).Key;
    }
}
