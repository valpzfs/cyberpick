using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public string itemID;
   public Sprite itemIcon; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Algo toco el objeto: " + other.name);
        if(other.CompareTag("Player"))
        {
            if(!TaskListLoader.CurrentExpectedItemID.Contains(itemID))
            {
                Debug.LogWarning("This item is not allowed in this warehouse!!");
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
}
