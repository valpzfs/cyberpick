using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
    public AudioSource soundEffect;
    public float delaySceneLoad = 1.0f;
    public void Iniciar()
    {
        StartCoroutine(PlaySoundThenLoad(1));
    }
    public void instrucciones()
    {
        StartCoroutine(PlaySoundThenLoad(11));
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
    public void Salir()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else 
            Application.Quit();
        #endif
    }

    
}