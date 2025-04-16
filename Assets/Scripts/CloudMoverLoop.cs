using UnityEngine;

public class CloudMoverLoop : MonoBehaviour
{
    public float speed;
    private RectTransform rectTransform;
    private float canvasWidth;
    private float cloudWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        //Get canvas Width
        Canvas canvas = GetComponentInParent<Canvas>();
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        canvasWidth = canvasRect.rect.width;

        //Get the width of the cloud
        cloudWidth = rectTransform.rect.width;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Moves the cloud to the right
        rectTransform.anchoredPosition += new Vector2(speed * Time.deltaTime, 0);

        //When its off the right side, reset to the left
        if(rectTransform.anchoredPosition.x > canvasWidth + cloudWidth/2)
        {
            rectTransform.anchoredPosition = new Vector2(-cloudWidth, rectTransform.anchoredPosition.y);
        }
        
    }
}
