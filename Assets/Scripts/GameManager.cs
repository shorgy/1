using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject deathScreen;  // ��������� �� ����� �����
    public InputField playerNameInput;  // ���� �������� ���� ������

    private ServerManager serverManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        serverManager = FindObjectOfType<ServerManager>();
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true);
        Time.timeScale = 0f;  // ��������� ��� � ��
    }

    public void SubmitScore()
    {
        string playerName = playerNameInput.text;
        int playerScore = Mathf.FloorToInt(FindObjectOfType<PlayerController>().transform.position.z);  // ������������� �������� ������� �� �������

        Debug.Log("Player Name: " + playerName);  
        Debug.Log("Player Score: " + playerScore);  

        serverManager.AddPlayer(playerName, playerScore);

        // ���������� ��� ��� ���������� � ������� ���� ���� �������� ���������� �����
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}