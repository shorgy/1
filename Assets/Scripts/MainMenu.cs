using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("1");  // ��'� ����� � ��������
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
    public void ViewLeaderboard()
    {
        SceneManager.LoadScene("LeaderboardScene");  // ��'� ����� � �������� �����
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");  // ��'� ����� � �������� ���� 
    }
}
