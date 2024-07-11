using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardController : MonoBehaviour
{
    private const string baseUrl = "http://localhost:3000/players";
    public Transform leaderboardPanel;
    public GameObject leaderboardEntryPrefab;

    void Start()
    {
        GetPlayers();
    }

    public void GetPlayers()
    {
        StartCoroutine(GetPlayersCoroutine());
    }

    private IEnumerator GetPlayersCoroutine()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(baseUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string jsonResult = www.downloadHandler.text;
                Debug.Log("Received JSON: " + jsonResult);

                // Оновлюємо формат JSON-відповіді
                try
                {
                    PlayerList playerList = JsonUtility.FromJson<PlayerList>(jsonResult);
                    List<PlayerData> players = playerList.players;
                    UpdateLeaderboard(players);
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error parsing JSON: " + e.Message);
                }
            }
        }
    }

    private void UpdateLeaderboard(List<PlayerData> players)
    {
        foreach (Transform child in leaderboardPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var player in players)
        {
            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardPanel);
            entry.GetComponent<Text>().text = $"{player.name}: {player.score}";
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int score;
    }

    [System.Serializable]
    public class PlayerList
    {
        public List<PlayerData> players;
    }
}
