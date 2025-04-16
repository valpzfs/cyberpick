using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject objectToDrag;
    public GameObject objectDragToPos;
    public float dropDistance;
    public bool isLocked = false;
    Vector2 objectInitPos;

    private bool hasNotified = false;

    void Start()
    {
        objectInitPos = objectToDrag.transform.position;
    }

    public void DragObject()
    {
        if (!isLocked)
        {
            objectToDrag.transform.position = Input.mousePosition;
        }
    }

    public void DropObject()
    {
        float Distance = Vector3.Distance(objectToDrag.transform.position, objectDragToPos.transform.position);
        if (Distance < dropDistance)
        {
            isLocked = true;
            objectToDrag.transform.position = objectDragToPos.transform.position;

            if (!hasNotified)
            {
                DragDropManager.Instance.NotifyCorrectDrop();
                hasNotified = true;
            }
        }
        else
        {
            objectToDrag.transform.position = objectInitPos;
        }
    }
}

