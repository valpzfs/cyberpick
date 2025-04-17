using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadSceneAsync(1);
    }

    public void EasyLevel(){
        PlayerPrefs.SetString("LastScene", "LevelSelector"); // Guarda de dónde viene
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync(2);
    }
}
