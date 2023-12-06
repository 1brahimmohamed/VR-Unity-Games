using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float maxX = 5.0f;

    void Update()
    {
        // Move the stone along the X-axis
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.right );

        // Destroy the stone if it moves outside the game frame on the right side
        if (transform.position.x > maxX)
        {
            Destroy(gameObject);
        }
    }
}