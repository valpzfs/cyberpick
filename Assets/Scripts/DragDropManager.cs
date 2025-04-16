using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragDropManager : MonoBehaviour
{
    public static DragDropManager Instance;

    public int totalDraggables;  // Total de objetos que se deben colocar
    private int completedCount = 0;

    public GameObject winPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        winPanel.SetActive(false); // Oculta el panel al inicio
    }

    public void NotifyCorrectDrop()
    {
        completedCount++;
        if (completedCount >= totalDraggables)
        {
            winPanel.SetActive(true); // Muestra el panel de victoria
            StartCoroutine(LoadScenewithDelay(1f)); //Espera 1 segundo
        }
    }
    private IEnumerator LoadScenewithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainScene");
    }
 }
