using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource soundEffect;
    public float delaySceneLoad = 1.0f;
    public void PlayGame(){
        //SceneManager.LoadSceneAsync(1);
        StartCoroutine(LoadBeforeDelay());
    }

    public void EasyLevel()
    {
        PlayerPrefs.SetString("LastScene", "LevelSelector"); // Guarda de dónde viene
        PlayerPrefs.Save();
        StartCoroutine(LoadBeforeDelay());
    }

    private IEnumerator LoadBeforeDelay()
    {
        // PlayerPrefs.SetString("LastScene", "LevelSelector"); // Guarda de dónde viene
        // PlayerPrefs.Save();
        if(soundEffect != null)
        {
            soundEffect.Play();
            yield return new WaitForSeconds(delaySceneLoad);
        }
        SceneManager.LoadSceneAsync(2);
        //SceneManager.LoadSceneAsync(2);
    }
}
