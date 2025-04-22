using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public GameObject message;
    public GameObject wall;
    void Start()
    {
        if(message != null)
        {
            message.SetActive(false);
            wall.SetActive(false);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D checPoint)
    {
        if(checPoint.CompareTag("Player"))
        {
            if(!GameManager.instance.TalkedtoSupervisor)
            {
                if(message != null)
                {
                    message.SetActive(true);
                    wall.SetActive(true);


                }
            }
            else
            {
                wall.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D checkpoint)
    {
        if(checkpoint.CompareTag("Player") && message != null)
        {
            message.SetActive(false); //Hides the messages when player is not touching the collider
        }
    }   
}
