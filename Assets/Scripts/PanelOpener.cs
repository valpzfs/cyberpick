using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    public AudioSource soundEffect;
    public float delaySceneLoad = 1.0f;

    void Start()
    {
        if (Panel != null)
        {
            Panel.SetActive(false); // Desactivar el panel al inicio
        }
    }

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive); // Alternar el estado del panel
             StartCoroutine(PlaySound());
        }
    }

    private IEnumerator PlaySound()
    {
        if(soundEffect != null)
        {
            soundEffect.Play();
            yield return new WaitForSeconds(delaySceneLoad);
        }
    }
}
