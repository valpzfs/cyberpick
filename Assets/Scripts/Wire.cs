using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Wire : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    public GameObject LightOn;
    Vector3 startPoint;
    Vector3 startPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 2f);
        foreach (Collider2D collider in colliders){
            //asegurar que el colider no  sea mi propio colider
            if(collider.gameObject != gameObject){
                UpdateWire(collider.transform.position);

                if (transform.parent.name.Equals(collider.transform.parent.name))
                {
                    MainMinigameCount.Instance.SwitchChange(1);

                    collider.GetComponent<Wire>()?.Done();
                    Done();
                }

                return;
            }
        }

       UpdateWire(newPosition);
    }

    void Done(){
        LightOn.SetActive(true);

        Destroy(this);
    }

    private void OnMouseUp()
    {
     //resetear posicion de mouse
        UpdateWire(startPosition);
    }
    void UpdateWire(Vector3 newPosition)
    {
         //actualizar cable
        transform.position = newPosition;

        //actualizar direccon
        Vector3 direction = newPosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        //actualizar escala
        float distance = Vector2.Distance(startPoint, newPosition);
        wireEnd.size = new Vector2(distance, wireEnd.size.y);

        
    }

    
}