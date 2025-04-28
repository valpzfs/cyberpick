using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuButton : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject pauseMenuPanel;

    public Slider _musicSlider, _sfxSlider;

    


    void Start()
    {
        CloseAllPanels();
        Time.timeScale = 1;
    }

    public void Pause()
    {
        bool isPaused = pauseMenuPanel.activeSelf;
        CloseAllPanels();
        pauseMenuPanel.SetActive(!isPaused);
        
    }

    public void Home()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    private void CloseAllPanels()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void ToggleMusic(){
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX(){
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume(){
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume(){
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
    }
}
