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

    public void EasyLevel(){
        StartCoroutine(PlaySoundThenLoad(2));
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
}
