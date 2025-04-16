using UnityEngine;

public class TriggerMinigame : MonoBehaviour
{
    public MinigameManager minigameManager;

    void OnMouseDown()
    {
        Debug.Log("Clicked minigame trigger object");
        if (minigameManager != null)
        {
            minigameManager.LoadRandomMinigame();
            GetComponent<Collider2D>().enabled = false; //disables after 1 use
        }
    }
}