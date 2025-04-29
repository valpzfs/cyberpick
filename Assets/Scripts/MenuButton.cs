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
        SceneManager.LoadScene(0);
    }

    private void CloseAllPanels()
    {
        pauseMenuPanel.SetActive(false);
    }

}
