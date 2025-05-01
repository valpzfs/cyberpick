using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class UserInfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI userInfoText; // Assign in Inspector
    private string apiUrl = "http://localhost:3000/api/user"; // https://cyberpick-web.vercel.app/api/user

    void Start()
    {
        StartCoroutine(FetchUserData());
    }

    IEnumerator FetchUserData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parse JSON response
                UserSession2 session = JsonUtility.FromJson<UserSession2>(request.downloadHandler.text);

                if (session.user != null)
                {
                    userInfoText.text = $"User: {session.user.name}\nEmail: {session.user.email}";
                }
                else
                {
                    userInfoText.text = "User not found.";
                }
            }
            else
            {
                userInfoText.text = "Please login!";
                Debug.LogError(request.error);
            }
        }
    }
}

// JSON model for user session
[System.Serializable]
public class UserSession2
{
    public User2 user;
}

[System.Serializable]
public class User2
{
    public string name;
    public string email;
}
