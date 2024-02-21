using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    // Declare a private float variable named moveSpeed and assign a default value of 2f
    [SerializeField] private float moveSpeed = 2f;

    // Declare private variables for the Rigidbody2D component and the movement direction
    private Rigidbody2D rb;
    private Vector2 moveDir;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the Rigidbody2D component attached to the same GameObject and assign it to rb
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called at a fixed interval, independent of frame rate
    private void FixedUpdate()
    {
        // Move the Rigidbody2D's position based on the movement direction, moveSpeed, and fixed delta time
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    // Method to update the movement direction
    public void MoveTo(Vector2 targetPosition)
    {
        // Update the movement direction to the specified target position
        moveDir = targetPosition;
    }


}
