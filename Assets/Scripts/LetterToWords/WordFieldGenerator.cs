using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordFieldGenerator : MonoBehaviour
{
    public GameObject word;
    public Transform parentOfWord;
    private int numberOfDefinedWords;
    private bool lowerUpper;
    
    private void Awake()
    {
        if (LevelManager.Instance.numberOfWords <= 6)
        {
            
            gameObject.GetComponent<GridLayoutGroup>().constraintCount = LevelManager.Instance.numberOfWords / 2;
        }
        else
            gameObject.GetComponent<GridLayoutGroup>().constraintCount = LevelManager.Instance.numberOfWords / 3;
    }
    private void Start()
    {
        GenerateWords();
    }

    private void GenerateWords()
    {
        bool minimumWord = false;
        var randomWord = RandomWord();
        var corectW = Random.Range(1, LevelManager.Instance.numberOfWords);

        if (LevelManager.Instance.upper && !LevelManager.Instance.lowerUpper)
            word.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.UpperCase;
        else if (LevelManager.Instance.upper && LevelManager.Instance.lowerUpper)
        {
            lowerUpper = true;
            if (Random.Range(0, 2) == 1 ? true : false) randomWord = randomWord.ToUpper();
            //randomWord = randomWord[0].ToString().ToUpper() + randomWord.Substring(1, randomWord.Length - 1);
        }

        word.GetComponent<TextMeshProUGUI>().text = randomWord;
        if (randomWord[0].ToString().Equals(LevelManager.Instance.definedLetter))
        {
            minimumWord = true;
            ++numberOfDefinedWords;
            LevelManager.Instance.numberOfDefinedWords = numberOfDefinedWords;
        }
        for (int i = 1; i < LevelManager.Instance.numberOfWords; i++)
        {
            var newWord = Instantiate(word, parentOfWord, true);
           
            if (i == corectW && !minimumWord)
            {
                var wordNumber = DictionaryReader.Instance.dictionary[LevelManager.Instance.definedLetter].Length - 1;
                newWord.GetComponent<TextMeshProUGUI>().text = DictionaryReader.Instance.dictionary[LevelManager.Instance.definedLetter][Random.Range(0, wordNumber)];
                ++numberOfDefinedWords;
                LevelManager.Instance.numberOfDefinedWords = numberOfDefinedWords;
            }
            else
            {
                randomWord = RandomWord();

                if (lowerUpper)
                    if (Random.Range(0, 2) == 1 ? true : false) randomWord = randomWord.ToUpper();

                newWord.GetComponent<TextMeshProUGUI>().text = randomWord;

                if (randomWord[0].ToString().Equals(LevelManager.Instance.definedLetter))
                {
                    minimumWord = true;
                    ++numberOfDefinedWords;
                    LevelManager.Instance.numberOfDefinedWords = numberOfDefinedWords;
                }
            }
        }
    }
    private string RandomWord()
    {
        var randomLetterNumber = Random.Range(0, 29);  
        var wordNumber = DictionaryReader.Instance.dictionary.ElementAt(randomLetterNumber).Value.Length - 1;
        string selectedWord;
        while (true)
        {
            var randomWordNumber = Random.Range(0, wordNumber);
            selectedWord = DictionaryReader.Instance.dictionary.ElementAt(randomLetterNumber).Value[randomWordNumber];

            if (selectedWord.Length < 9) break;
        }
        
        return selectedWord;
    }
}
