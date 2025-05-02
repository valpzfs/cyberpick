using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WireTask : MonoBehaviour
{
    public int currentConnections;
    public GameObject victoryPanel; 
    public GameObject instructionPanel; // el panel de instrucciones

    public void HideInstructions()
    {
        instructionPanel.SetActive(false); // esconde el panel
    }

    public void provingVictory()
    {
        if (currentConnections == 4)
        {
            victoryPanel.SetActive(true); 
            Debug.Log("Player won quiz!, Finished");
            GameManager.instance.itemsWon.Add(GameManager.instance.currentItemID);
            GameManager.instance.SumPoints(10);
            StartCoroutine(finishAttempt());
        }
    }
    private IEnumerator finishAttempt()
    {
        yield return StartCoroutine(attempt(true));
        yield return StartCoroutine(LoadSceneDelay(0.8f));
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
    private IEnumerator LoadSceneDelay(float delay)
  {
    yield return new WaitForSeconds(delay);

    string lastScene = PlayerPrefs.GetString("LastScene", "MainLevel1Part");
    SceneManager.LoadScene(lastScene);
  }
    
}

