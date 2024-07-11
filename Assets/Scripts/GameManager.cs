using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject deathScreen;  // Посилання на екран смерті
    public InputField playerNameInput;  // Поле введення імені гравця

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
        Time.timeScale = 0f;  // Зупиняємо час у грі
    }

    public void SubmitScore()
    {
        string playerName = playerNameInput.text;
        int playerScore = Mathf.FloorToInt(FindObjectOfType<PlayerController>().transform.position.z);  // Використовуємо пройдену відстань як рахунок

        Debug.Log("Player Name: " + playerName);  
        Debug.Log("Player Score: " + playerScore);  

        serverManager.AddPlayer(playerName, playerScore);

        // Перезапуск гри або повернення в головне меню після успішного надсилання даних
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}