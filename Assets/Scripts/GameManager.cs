using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int score = 0;
    public TextMeshProUGUI textscore;

    public HashSet<string> itemsWon = new HashSet<string>();
    public string currentItemID;
    public bool TalkedtoSupervisor = false;

    void Awake()
    {
        if(instance == null)
        {
            Debug.Log("GameManager instance created.");
            instance = this;
            DontDestroyOnLoad(gameObject); //Persiste entre escenas
            itemsWon = new HashSet<string>();
            SceneManager.sceneLoaded += OnSceneLoaded; // escucha cuando se cargue escena nueva
        }
        else
        {
            Destroy(gameObject); //Evita duplicados
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded: " + scene.name);
        //Debug.Log("Items obtained so far: " + string.Join(", ", itemsWon));

        if(scene.name != "MainMenu" || scene.name != "QuizMinigame" || scene.name != "WireMiniGame" || scene.name != "LevelSelector" || scene.name != "MainLevel1Part")
        {
            foreach(string itemID in itemsWon)
            {
                GameObject itemObj = GameObject.Find(itemID);
                if(itemObj != null)
                {
                    itemObj.SetActive(true);
                }
            }
        }
        GameObject textObj = GameObject.Find("Score");
        if(textObj != null)
        {
            textscore = textObj.GetComponent<TextMeshProUGUI>();
            ActualizartextScore();
        }

        // Solo resetea si estás en la escena MainScene
        if (scene.name == "MainScene")
        {
            PlayerPrefs.DeleteKey("InitialDialogueShown");
            PlayerPrefs.Save();
            ReiniciarPuntaje();
            TalkedtoSupervisor = false;

        }
    }

    public void SumPoints(int cantidad)
    {
        score += cantidad;
        ActualizartextScore();
    }
    public void ActualizartextScore()
    {
        if(textscore != null)
        {
            textscore.text = "Score: " + score;
        }
    }

    public void ReiniciarPuntaje()
    {
        score = 0;
        ActualizartextScore();
    }

    // API communication

    private string apiUrl = "http://localhost:3000/api/";
    private int userId;
    private DateTime matchStartTime;
    private int currentMatchId;

    public IEnumerator FetchUserData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl + "user"))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                UserSession session = JsonUtility.FromJson<UserSession>(request.downloadHandler.text);

                if (session.user != null)
                {
                    userId = int.Parse(session.user.id);
                    Debug.Log("User ID: " + userId);
                }
            }
            else
            {
                Debug.LogError("Failed to fetch user data: " + request.error);
            }
        }
    }

    public IEnumerator CreateMatch(int levelId)
    {
        Debug.Log("Creating match for level ID: " + levelId);

        CreateMatchClass match = new CreateMatchClass
        {
            ID_level = levelId
        };

        string json = JsonUtility.ToJson(match);
        Debug.Log("Match JSON: " + json);
        using (UnityWebRequest request = new UnityWebRequest(apiUrl + "match", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            try
            {

                if (request.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Match created successfully.");
                    string jsonResponse = request.downloadHandler.text;
                    MatchResponse response = JsonUtility.FromJson<MatchResponse>(jsonResponse);
                    currentMatchId = response.matchId;
                    Debug.Log("Match ID: " + currentMatchId);
                }
                else
                {
                    Debug.LogError("Failed to create match: " + request.error);
                    Debug.LogError("Response code: " + request.responseCode);
                    Debug.LogError("Response: " + request.downloadHandler.text);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Exception during match creation: " + ex.Message);
            }
        }
    }

    public IEnumerator EndMatch(int levelId)
    {
        if (currentMatchId == 0)
        {
            Debug.LogError("No match created, can't end match.");
            yield break;
        }

        var endTime = DateTime.Now;
        var duration = (int)(endTime - matchStartTime).TotalSeconds;

        MatchEndClass match = new MatchEndClass
        {
            matchId = currentMatchId,
            score = score,
        };

        string json = JsonUtility.ToJson(match);

        // Now update the existing match using currentMatchId
        using (UnityWebRequest request = new UnityWebRequest(apiUrl + "match", "PUT"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Match ended and updated successfully.");
            }
            else
            {
                Debug.LogError("Failed to update match: " + request.error);
            }
        }
    }

    public IEnumerator AddAttempt(bool isCorrect)
    {
        // if (currentMatchId == 0)
        // {
        //     Debug.LogError("No match created, can't add attempt.");
        //     yield break;
        // }

        AttemptClass attempt = new AttemptClass
        {
            matchId = currentMatchId,
            isCorrect = isCorrect,
        };

        string json = JsonUtility.ToJson(attempt);

        // Now add the attempt using currentMatchId
        using (UnityWebRequest request = new UnityWebRequest(apiUrl + "attempt", "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Attempt added successfully.");
            }
            else
            {
                Debug.LogError("Failed to add attempt: " + request.error);
            }
        }
    }
}

[System.Serializable]
public class UserSession
{
    public User user;
}

[System.Serializable]
public class User
{
    public string id;
}

[System.Serializable]
public class CreateMatchClass
{
    public int ID_level;
}

[System.Serializable]
public class MatchEndClass
{
    public int matchId;
    public int score;
}

[System.Serializable]
public class MatchResponse
{
    public string message;
    public int matchId;
}

[System.Serializable]
public class AttemptClass
{
    public int matchId;
    public bool isCorrect;
}