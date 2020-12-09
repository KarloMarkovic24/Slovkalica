using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int level, score, 
        numberOfLetters,numberOfDefinedLetters,
        numberOfWords, numberOfDefinedWords,
        numberOfWordsNumbers,currentNumber;
    public string definedLetter, playerName,equation;
    public int[] number = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] selectedWord = new int[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public bool upper, lowerUpper;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;

            upper = false;
            lowerUpper = false;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void WritePlayerScore()
    {
        DictionaryWriter.Instance.WriteScore(playerName, score);
    }
}
