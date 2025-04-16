using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public float speed = 20f;          // How fast the cloud moves
    public float resetX = 1000f;       // Where the cloud resets to (offscreen right)
    public float startX = -1000f;      // Where it moves to (offscreen left)

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (transform.position.x > resetX)
        {
            Vector3 pos = transform.position;
            pos.x = startX;
            transform.position = pos;
        }
    }
}

