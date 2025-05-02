using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.XR;
using TMPro;
using UnityEngine.SceneManagement;

public class AdminPreg : MonoBehaviour
{
    public static AdminPreg Instance;
    public List<PregResp> ListPreg;
    public GameObject [] opciones;
    public int pregActual;
    public TextMeshProUGUI textoPreg;
    int cont;
    public GameObject questionBox; //UI panel in inspector
    public GameObject instructions; //UI panel in inspector
    private int correctAnswers = 0;
    
    void Start()
    {
        instructions.SetActive(true);
        //ShowQuestionBox();

    }

    public void HideInstructions()
    {
        instructions.SetActive(false); // esconde el panel
        ShowQuestionBox();
    }

    public void ShowQuestionBox()
    {
        questionBox.SetActive(true);
        cont = 0;
        generarPregunta();
    }

    void generarPregunta()
    {
        //El minijuego de preguntas tendra dos preguntas por item, una vez que haya respondido el 
        // minijuego desaparece y regresa a la pantalla principal 
        if (cont < 2)
        {
            pregActual = Random.Range(0, ListPreg.Count);
            textoPreg.text = ListPreg[pregActual].Preg;
            establecerRespuesta(); 
        }
        else
        {
            gameObject.SetActive(false);
            string lastScene = PlayerPrefs.GetString("LastScene", "MainLevel1Part");
            SceneManager.LoadScene(lastScene);
        }
    }
    void establecerRespuesta()
    {
        for(int i = 0; i < opciones.Length; i++)
        {
            //Reiniciar texto, color y respuesta
            Respuesta r = opciones[i].GetComponent<Respuesta>();
            r.esCorrecto = false;
            opciones[i].GetComponent<UnityEngine.UI.Image>().color = Color.white;
        
            if(i < ListPreg[pregActual].Resp.Length)
            {
                opciones[i].SetActive(true);
                opciones[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ListPreg[pregActual].Resp[i];

                if(ListPreg[pregActual].respCorrecta == i)
                {
                    r.esCorrecto = true;
                }
            }
            else
            {
              opciones[i].SetActive(false); //Oculta botones extra  
            }
        }
    }

    public void Siguiente(bool wasCorrect)
    {
        if(wasCorrect)
        {
            correctAnswers++;
        }
        ListPreg.RemoveAt(pregActual);
        cont++; //Aumentar antes de generar nueva pregunta
        if(cont == 2)
        {
            //Win Condition
            if(correctAnswers == 2)
            {
                StartCoroutine(attempt(true));
                GameManager.instance.itemsWon.Add(GameManager.instance.currentItemID); 
                //Debug.Log("Item ganado: " + GameManager.instance.currentItemID);
            }
            else
            {
                StartCoroutine(attempt(false));
                Debug.Log("Player did not Win, try Again, Finished");
            }
            generarPregunta();
        }
        else
        {
            generarPregunta();
        }
    } 

    private IEnumerator attempt(bool correct)
    {
        if (GameManager.instance != null)
        {
            yield return StartCoroutine(GameManager.instance.AddAttempt(correct));
        } else {
            Debug.LogError("GameManager instance is null. Cannot fetch user data.");
        }
    }
}
