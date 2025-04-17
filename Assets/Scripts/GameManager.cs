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

    //Minigame result tracking
    public string triggerObjectID; //example batteryInv
    public bool minigameWon = false;
    public HashSet<string> itemsWon = new HashSet<string>();

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
        Debug.Log("Items won so far: " + string.Join(", ", itemsWon));
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
