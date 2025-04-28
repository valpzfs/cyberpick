using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.ComponentModel;
//using UnityEditor.Search;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
 
    private Dictionary<string, GameObject> activeSlots = new Dictionary<string, GameObject>();
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

        } 


        else Destroy(gameObject);
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        //Reset only if returning to the main menu
        if(sceneName == "MainScene")
        {
            ResetInventory();
            gameObject.SetActive(false);
            return;
        }

        if(sceneName.StartsWith("Bodega_"))
        {
            Debug.Log("Inventario Activo en: " + sceneName);
            gameObject.SetActive(true);
            
        } 
        else
        {
            Debug.Log("Inventario oculto en: " + sceneName);
            gameObject.SetActive(false);
        }
        
    


    }

    public void AddItemToInventory(string itemID, Sprite icon)
    {
        if(activeSlots.ContainsKey(itemID)) return;

        GameObject slot = Instantiate(slotPrefab, inventoryPanel.transform);
        Image iconImage = slot.transform.Find("ItemIcon").GetComponent<Image>();
        iconImage.sprite = icon;
        activeSlots[itemID] = slot;

        
    }

    public void ResetInventory()
    {
        //Borrar todos los slots visibles
        foreach(var slot in activeSlots.Values)
        {
            Destroy(slot);
        }
        activeSlots.Clear();

        //Borrar items ganados
        if(GameManager.instance != null)
        {
            GameManager.instance.itemsWon.Clear();
            GameManager.instance.currentItemID = null;
        }
        Debug.Log("Inventroy and ItemsWon reset");
    }
}
