using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
public Transform defaultSpawnPoint;
//public Transform spwanFromMinigame;
    void Start()
    {
        string LastScene = PlayerPrefs.GetString("LastScene", "");

        if(LastScene == "LevelSelector")
        {
            transform.position = defaultSpawnPoint.position;
        }
        PlayerPrefs.DeleteKey("LastScene");
        
    }
}
