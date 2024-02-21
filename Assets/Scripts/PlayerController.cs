using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Declare a public class named PlayerController that inherits from MonoBehaviour
public class PlayerController : MonoBehaviour
{
    // Declare a private float variable named moveSpeed and assign a default value of 1f
    [SerializeField] private float moveSpeed = 1f;

    // Declare private variables for player controls, movement input, the Rigidbody2D component, and the animations
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Initialize playerControls with a new instance of PlayerControls
        playerControls = new PlayerControls();
        // Get the Rigidbody2D component attached to the same GameObject and assign it to rb
        rb = GetComponent<Rigidbody2D>();
        // Get the Animator component attached to the same GameObject and assign it to myAnimator
        myAnimator = GetComponent<Animator>();
        // Get the SpriteRenderer component attached to the same GameObject and assign it to mySpriteRender
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        // Enable the input actions associated with playerControls
        playerControls.Enable();
    }

    // Update is called once per frame
    private void Update()
    {
        // Call PlayerInput method to handle player input
        PlayerInput();
    }

    // FixedUpdate is called at a fixed interval, independent of frame rate
    private void FixedUpdate()
    {
        // Adjust the player's facing direction based on the mouse position
        AdjustPlayerFacingDirection();
        // Call Move method to handle player movement
        Move();
    }

    // Method to handle player input
    private void PlayerInput()
    {
        // Read the movement input value from the PlayerControls input system and assign it to the movement variable
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        // Set animation parameters based on movement input
        myAnimator.SetFloat("MoveX", movement.x);
        myAnimator.SetFloat("MoveY", movement.y);
    }

    // Method to handle player movement
    private void Move()
    {
        // Move the Rigidbody2D's position based on the movement input, moveSpeed, and fixed delta time
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    // Method to adjust the player's facing direction based on the mouse position
    private void AdjustPlayerFacingDirection()
    {
        // Get the mouse position
        Vector3 mousePos = Input.mousePosition;
        // Convert the player's position to screen coordinates
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // Check if the mouse is to the left or right of the player and flip the sprite accordingly
        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true;
        }
        else
        {
            mySpriteRender.flipX = false;
        }
    }

}


