using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    void OnCollisionEnter(Collision collision) 
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);
        
        if (collision.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Jugador detectado. Cambiando de escena...");
            SceneManager.LoadScene(7);
        }
    }
}



