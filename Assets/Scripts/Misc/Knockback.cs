using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    // Boolean to track whether the entity is currently being knocked back
    public bool gettingKnockedBack { get; private set; }

    // Serialized field to determine the duration of knockback
    [SerializeField] private float knockBackTime = .2f;

    // Reference to the Rigidbody2D component of the entity
    private Rigidbody2D rb;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the Rigidbody2D component attached to the same GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    // Method to apply knockback force to the entity
    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        // Set gettingKnockedBack flag to true
        gettingKnockedBack = true;

        // Calculate the direction and magnitude of knockback force
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;

        // Apply the knockback force to the entity
        rb.AddForce(difference, ForceMode2D.Impulse);

        // Start the coroutine to handle knockback duration
        StartCoroutine(KnockRoutine());
    }

    // Coroutine to handle knockback duration
    private IEnumerator KnockRoutine()
    {
        // Pause the coroutine execution for a specified duration
        yield return new WaitForSeconds(knockBackTime);

        // Set the velocity of the entity to zero to stop knockback movement
        rb.velocity = Vector2.zero;

        // Reset the gettingKnockedBack flag to false
        gettingKnockedBack = false;
    }
}


