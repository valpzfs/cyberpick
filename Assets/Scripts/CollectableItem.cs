using System.Collections;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemID;
    public Sprite itemIcon; 
    public GameObject wrongItem;
    public AudioSource soundEffect;
    public float delaySceneLoad = 1.0f;

    public void Start()
    {
        wrongItem.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Algo toco el objeto: " + other.name);
        if(other.CompareTag("Player"))
        {
            if(TaskListLoader.CurrentExpectedItemID == null || TaskListLoader.CurrentExpectedItemID.Count == 0 || !TaskListLoader.CurrentExpectedItemID.Contains(itemID))
            {
                Debug.LogWarning("This item is not allowed in this warehouse!!");
                //wrongItem.SetActive(true);
                GameManager.instance.SumPoints(-5);
                StartCoroutine(WrongItemMessage());
                return;
            }
            Debug.Log("Item Picked up: " + itemID);

            //Add item to Inventory
            if(InventoryManager.instance != null)
            {
                InventoryManager.instance.AddItemToInventory(itemID, itemIcon);
                StartCoroutine(PlaySound());
            }

            //Remove from scene
            Destroy(gameObject);
        }
    }

    private IEnumerator PlaySound()
    {
        if(soundEffect != null)
        {
            soundEffect.Play();
            yield return new WaitForSeconds(delaySceneLoad);
        }
    }
    private IEnumerator WrongItemMessage()
    {
        wrongItem.SetActive(true);
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        wrongItem.SetActive(false);
    }

    
}
