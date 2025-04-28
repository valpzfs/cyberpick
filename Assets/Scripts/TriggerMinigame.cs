//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TriggerMinigame : MonoBehaviour
{
    public MinigameManager minigameManager;
    public GameObject itemImage;
    public string itemID;
    
     void Start()
    {
        //Checks if the item was already won
        if(GameManager.instance != null && GameManager.instance.itemsWon.Contains(itemID))
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
        if(GameManager.instance != null)
        {
            GameManager.instance.currentItemID = itemID;
        }

        if(minigameManager != null)
        {
            minigameManager.LoadRandomMinigame(gameObject);
        }
    }

    public void OnMinigameWon()
    {
        Debug.Log("Activando item: " + itemID);
        if(itemImage != null)
        {
            itemImage.SetActive(true);
        }
        // if(GameManager.instance != null)
        // {
        //     GameManager.instance.itemsWon.Add(itemID);
        // }
    }
}