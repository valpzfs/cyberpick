//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Respuesta : MonoBehaviour
{
    public bool esCorrecto = false;
    public AdminPreg pregAleatorias;
    public Color colorCorrecto = Color.green;
    public Color colorIncorrecto = Color.red;
    private bool yaRespondido = false;
    

    public void EnviarRespuesta()
    {
        if(yaRespondido) return; //No permite multiples clics
        yaRespondido = true;

        Image botonImg = GetComponent<Image>();

        if(esCorrecto)
        {
            //Debug.Log("Respuesta Correcta");
            if(botonImg != null) botonImg.color = colorCorrecto;
            //To check each scene Individually
            if (GameManager.instance != null)
            {
                GameManager.instance.SumPoints(5);
            }
            
            //pregAleatorias.SumPoints(5);
            
        } 
        else
        {
            //Debug.Log("Respuesta Incorrecta ");
            if(botonImg != null) botonImg.color = colorIncorrecto;            
            //Por cada respuesta incorrecta restar 5 puntos
            if(GameManager.instance != null)
            {
                GameManager.instance.SumPoints(-5);
            }
            
        }
        Invoke(nameof(SiguienteConDelay), 0.5f);
    }

    void SiguienteConDelay()
    {
        pregAleatorias.Siguiente();
        yaRespondido = false;
    }
}
