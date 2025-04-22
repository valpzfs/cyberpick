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
        StartCoroutine(PlaySoundThenLoad());
    }
    private IEnumerator PlaySoundThenLoad()
    {
        if(soundEffect != null)
        {
            soundEffect.Play();
            yield return new WaitForSeconds(delaySceneLoad);
        }
        SceneManager.LoadScene(1);
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