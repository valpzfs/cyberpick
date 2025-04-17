using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TriggerMinigame : MonoBehaviour
{
    public MinigameManager minigameManager;
    public GameObject itemImage;
    
     void Start()
    {
        // Hide the item image at the start
        if (GameManager.instance != null && GameManager.instance.minigameWon &&
            GameManager.instance.triggerObjectID == gameObject.name)
        {
            itemImage.SetActive(true);
        }
        else
        {
            itemImage.SetActive(false);
        }
    }
    void OnMouseDown()
    {

        Debug.Log("Clicked minigame trigger object");
        GameManager.instance.triggerObjectID = gameObject.name;

        if (minigameManager != null)
        {
            minigameManager.LoadRandomMinigame(gameObject); 
        }
    }
}