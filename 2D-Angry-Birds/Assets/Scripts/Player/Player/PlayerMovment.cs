using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float maxMoveSpeed = 10.0f;
    public float minMoveSpeed = 5.0f;
    public float minX = -5.0f;
    public float maxX = 5.0f;
    public float minY = -5.0f;
    public float maxY = 5.0f;

    void Update()
    {
        // Get arrow key inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate new position based on input
        float moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        
        Vector3 newPosition = transform.position +
                              moveSpeed * 
                              Time.deltaTime * 
                              new Vector3(horizontalInput, verticalInput, 0.0f);

        // Restrict the player within the defined boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Update the player's position
        transform.position = newPosition;
    }
}
