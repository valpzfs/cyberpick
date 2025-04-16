using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
  }
}
