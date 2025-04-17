using UnityEngine;

public class TriggerMinigame : MonoBehaviour
{
    public MinigameManager minigameManager;
    private bool hasBeenUsed = false; //Evita multiples clics
    void OnMouseDown()
    {
        
        if (hasBeenUsed) return;

        hasBeenUsed = true;
        Debug.Log("Clicked minigame trigger object");

        if (minigameManager != null)
        {
            minigameManager.LoadRandomMinigame(gameObject); //pasar el objecto al que se hizo clic
        }
    }
}