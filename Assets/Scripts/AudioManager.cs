using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
  [Header("Audio source")]
  [SerializeField] AudioSource musicSource;
  [SerializeField] AudioSource SFXSource;

  [Header("Audio clips")]
  public static AudioManager instance = null;
  public AudioClip [] background;
  public AudioClip sfxitem;
  public AudioClip sfxpanel;
  public AudioClip audio4;


    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //Persiste entre escenas
        }
        else
        {
            Destroy(gameObject); //Evita duplicados
        }
    }
    void OnEnable() 
    {
    SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        PlaySceneMusic(scene.name);
    }

    public void PlaySceneMusic(string sceneName) 
    {
        switch (sceneName)
        {
            case "MainScene":
                musicSource.clip = background[0];
                break;
            case "MainLevel1Part":
                musicSource.clip = background[1];
                break;
            case "Bodega_A1":
            case "Bodega_B1":
            case "Bodega_C1":
            case "Bodega_D1":
                musicSource.clip = background[2];
                break;
            default:
                musicSource.clip = background[1];
                break;
        }

        if (musicSource.clip != null && !musicSource.isPlaying)
            musicSource.Play();
    }

    private void PlaySFX(AudioClip clip){
        SFXSource.PlayOneShot(clip);
    }
}