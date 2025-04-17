using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMinigameCount : MonoBehaviour
{
    static public MainMinigameCount Instance;

    public int switchCount;
    public GameObject winText;
    private int onCount =0;
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

    winText.SetActive(false); // Asegurar que el texto está oculto al inicio
}

    public void SwitchChange(int points){
    onCount = onCount+ points;
    if(onCount == switchCount){
        winText.SetActive(true);
        StartCoroutine(LoadSceneDelay(0.8f));
    }
  }

  private IEnumerator LoadSceneDelay(float delay)
  {
    yield return new WaitForSeconds(delay);

    string lastScene = PlayerPrefs.GetString("LastScene", "MainLevel1Part");
    SceneManager.LoadScene(lastScene);
  }
}
