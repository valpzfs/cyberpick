using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public void Awake()
    {
        if(Instance == null){
            Instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme"); //aqui se pone el nombre de la cancion que queramos que sea theme pones el nombre del clip puesto en el inspector
    //para poner sfx en alguna interacción ponerlo en los otros codigos como AudioManager.Instance.PlaySFX("nombre del archivo o nombre que le pusiste al sfx")
    }

    public void PlayMusic(string name){
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null){
            Debug.Log("Sound not found");
        }else{
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void playSFX(string name){
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null){
            Debug.Log("Sound not found");
        }else{
            sfxSource.PlayOneShot(s.clip);
        }

    }

    public void ToggleMusic()
    {
        musicSource.mute=!musicSource.mute;

    }
    public void ToggleSFX(){
        sfxSource.mute=!sfxSource.mute;
    }

    public void MusicVolume(float volume){
        musicSource.volume = volume;
    }
    public void SFXVolume (float volume){
        sfxSource.volume = volume;
    }
}
