using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public ServerManager serverManager;
    public InputField nameInputField;
    public Text scoreText;
    public int playerScore;

    void Start()
    {
        // �������������
        playerScore = 0;
        UpdateScoreText();
    }

    public void OnSubmitButtonClicked()
    {
        string playerName = nameInputField.text;
        serverManager.AddPlayer(playerName, playerScore);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + playerScore.ToString();
    }

    // ����� ��� ���������� ����� (��������, ��� ����� �������)
    public void IncreaseScore(int amount)
    {
        playerScore += amount;
        UpdateScoreText();
    }
}