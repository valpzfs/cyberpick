using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MainMenu : MonoBehaviour
{
    public AudioSource soundEffect;
    public float delaySceneLoad = 1.0f;
    public void PlayGame(){
        SceneManager.LoadSceneAsync(1);
    }

    public void EasyLevel()
    {
        StartCoroutine(RunEasyLevel());
    }

    private IEnumerator RunEasyLevel()
    {
        yield return StartCoroutine(FetchUser());
        yield return StartCoroutine(CreateMatch(1));
        yield return StartCoroutine(PlaySoundThenLoad(2));
    }


    public void Back()
    {
        StartCoroutine(PlaySoundThenLoad(0));
    }


    private IEnumerator PlaySoundThenLoad(int sceneIndex)
    {
        if(soundEffect != null)
        {
            soundEffect.Play();
            yield return new WaitForSeconds(delaySceneLoad);
        }
        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator FetchUser()
    {
        if (GameManager.instance != null)
        {
            yield return StartCoroutine(GameManager.instance.FetchUserData());
        } else {
            Debug.LogError("GameManager instance is null. Cannot fetch user data.");
        }
    }

    private IEnumerator CreateMatch(int levelId)
    {
        if (GameManager.instance != null)
        {
            yield return StartCoroutine(GameManager.instance.CreateMatch(levelId));
        } else {
            Debug.LogError("GameManager instance is null. Cannot fetch user data.");
        }
    }
}
