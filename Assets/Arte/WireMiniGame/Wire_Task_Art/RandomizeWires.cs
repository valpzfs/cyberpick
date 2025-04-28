using UnityEngine;

public class RandomizeWires : MonoBehaviour
{
    private void Awake()
    {
        for ( int i = 0; i < transform.childCount; i++){
            GameObject currentCable = transform.GetChild(i).gameObject;
            GameObject otherCable = transform.GetChild(Random.Range(0, transform.childCount)).gameObject;

            Vector2 newPosCurrentCable = otherCable.transform.position;
            Vector2 newPosOtherCable = currentCable.transform.position;

            currentCable.transform.position = newPosCurrentCable;
            otherCable.transform.position = newPosOtherCable;
        }   
    }
}
