using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void MainMenu(int x)
    {
        switch (x)
        {
            case (1):
                SceneManager.LoadScene("Player");
                break;
            case (2):
                SceneManager.LoadScene("Settings");
                break;
            case (3):
                SceneManager.LoadScene("Scoreboard");
                break;
            case (0):
                Application.Quit();
                break;
        }
    }
    public void LetterGamesMenu(int x)
    {
        switch (x)
        {
            case (0):
                SceneManager.LoadScene("Main Menu");
                break;
            case (1):
                LevelManager.Instance.level = 1;
                LevelManager.Instance.numberOfLetters = 4;
                SceneManager.LoadScene("Letters");
                break;
            case (2):
                LevelManager.Instance.level = 1;
                LevelManager.Instance.numberOfWords = 2;
                SceneManager.LoadScene("LetterToWord");
                break;
            case (3):
                LevelManager.Instance.level = 1;
                SceneManager.LoadScene("SpellingImage");
                break;
            case (4):
                LevelManager.Instance.level = 1;
                LevelManager.Instance.numberOfWordsNumbers = 2;
                SceneManager.LoadScene("Numbers");
                break;
            case (5):
                LevelManager.Instance.level = 1;
                LevelManager.Instance.numberOfWordsNumbers = 2;
                SceneManager.LoadScene("Equation");
                break;
        }
    }
    public void PlayMenu()
    {
        SceneManager.LoadScene("Letter Game Menu");
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
