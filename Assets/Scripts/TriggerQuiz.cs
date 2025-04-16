using UnityEngine;

public class TriggerQuiz : MonoBehaviour
{
    public AdminPreg quizManager; //Object with AdminPreg in Inspector

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     //Cuando el jugador se 
    //     Debug.Log("Colisión detectada con: " + other.gameObject.name);
    //     if(other.CompareTag("Player"))
    //     {
    //         quizManager.ShowQuestionBox();
    //         GetComponent<Collider2D>().enabled = false; // disables this object
    //     }
    // }

    void OnMouseDown()
    {
        Debug.Log("Se hizo clic en el item");
        quizManager.ShowQuestionBox();
        GetComponent<Collider2D>().enabled = false; //disables this object so the player can't try again
    }
}
