using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ServerManager : MonoBehaviour
{
    private const string baseUrl = "http://localhost:3000/players";

    public void GetPlayers()
    {
        StartCoroutine(GetPlayersCoroutine());
    }

    public void AddPlayer(string name, int score)
    {
        StartCoroutine(AddPlayerCoroutine(name, score));
    }

    private IEnumerator GetPlayersCoroutine()
    {
        UnityWebRequest www = UnityWebRequest.Get(baseUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            // Обробка даних
        }
    }

    private IEnumerator AddPlayerCoroutine(string name, int score)
    {
        var postData = new Dictionary<string, string>
        {
            { "name", name },
            { "score", score.ToString() }
        };

        string json = JsonUtility.ToJson(postData);
        UnityWebRequest www = new UnityWebRequest(baseUrl, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Player added successfully");
            Debug.Log(www.downloadHandler.text);
        }
    }
}