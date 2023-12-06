using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public int screenMinX = -5;

    void Update()
    {
        // Move the enemy along the X-axis
        transform.Translate(- (moveSpeed * Time.deltaTime * Vector3.right));

        // Check if the enemy is outside the game frame on the right side
        if (IsOutsideGameFrame())
        {
            Destroy(gameObject); // Destroy the enemy
        }
    }

    bool IsOutsideGameFrame()
    {
        
        if (transform.position.x < screenMinX)
        {
            return true; // Enemy is outside the game frame
        }

        return false; // Enemy is still within the game frame
    }
}