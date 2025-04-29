using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class ForkLiftMov : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    public float speed;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    void Update()
    {
        Vector2 direction = (currentPoint.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);

        // Check if we reached the target point
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            rb.linearVelocity = Vector2.zero; // Stop movement
            SwapTarget(); // Change direction
        }
    }

    private void SwapTarget()
    {
        if (currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        else
        {
            currentPoint = pointB.transform;
        }
        Flip();
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }


}
