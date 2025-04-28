using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CableAttempt : MonoBehaviour
{
    public SpriteRenderer cableEnd;
    public GameObject Light;
    private Vector2 originalPosition;
    private Vector2 originalSize;
    private WireTask wiretask;


    void Start()
    {
        originalPosition = transform.position;
        originalSize = cableEnd.size;
        wiretask = transform.root.gameObject.GetComponent<WireTask>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
           resetCable();
            
        }
    }

    private void OnMouseDrag()
    {
        UpdatePosition();
        TryConnection();
        UpdateRotation();
        UpdateSize();
    }

    private void UpdatePosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    private void UpdateRotation()
    {
        Vector2 currentPosition = transform.position;
        Vector2 originPoint = transform.parent.position;

        Vector2 direction = currentPosition - originPoint;
        float angle = Vector2.SignedAngle(Vector2.right * transform.lossyScale, direction); 
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void UpdateSize()
    {
        Vector2 currentPosition = transform.position;
        Vector2 originPoint = transform.parent.position;
        float distance = Vector2.Distance(currentPosition, originPoint);

        cableEnd.size = new Vector2(distance, cableEnd.size.y);
    }

    private void resetCable()
    {
        transform.position = originalPosition;
        transform.rotation = Quaternion.identity;
        cableEnd.size = originalSize;
    }

    private void TryConnection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.4f);

        foreach (Collider2D col in colliders)
        {
            if (col.gameObject != gameObject)
            {
                transform.position = col.transform.position;

                CableAttempt otherCable = col.gameObject.GetComponent<CableAttempt>();

                if(cableEnd.color == otherCable.cableEnd.color){
                    Connection();
                    otherCable.Connection();

                    wiretask.currentConnections++;
                    wiretask.provingVictory();
                }
            }
        }
    
    }

    public void Connection()
    {
        Light.SetActive(true);
        Destroy(this);
    }
}
