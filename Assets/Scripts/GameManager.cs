using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        Debug.Log("Items obtained so far: " + string.Join(", ", itemsWon));

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
}
