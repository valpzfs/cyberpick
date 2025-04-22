using System.Collections;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemID;
   public Sprite itemIcon; 
   public GameObject wrongItem;
    public void Start()
    {
        wrongItem.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Algo toco el objeto: " + other.name);
        if(other.CompareTag("Player"))
        {
            if(!TaskListLoader.CurrentExpectedItemID.Contains(itemID))
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
            }

            //Remove from scene
            Destroy(gameObject);
        }
    }
    private IEnumerator WrongItemMessage()
    {
        wrongItem.SetActive(true);
        yield return new WaitForSeconds(3f);
        wrongItem.SetActive(false);
    }
}
