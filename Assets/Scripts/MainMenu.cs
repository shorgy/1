using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("1");  // Ім'я сцени з геймплеєм
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    public void ViewLeaderboard()
    {
        SceneManager.LoadScene("LeaderboardScene");  // Ім'я сцени з таблицею лідерів
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Ім'я сцени з головним меню 
    }
}
