using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;

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
        }
    }
}
