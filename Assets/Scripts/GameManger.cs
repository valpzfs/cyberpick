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

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //Persiste entre escenas
            SceneManager.sceneLoaded += OnSceneLoaded; // escucha cuando se cargue escena nueva
        }
        else
        {
            Destroy(gameObject); //Evita duplicados
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject textObj = GameObject.Find("Score");
        if(textObj != null)
        {
            textscore = textObj.GetComponent<TextMeshProUGUI>();
            ActualizartextScore();
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
