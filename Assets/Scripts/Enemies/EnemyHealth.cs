using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Private field to store the starting health of the object, serialized for inspection in the Unity Inspector.
    [SerializeField] private int startingHealth = 3;

    // Serialized field to reference the death visual effects prefab.
    [SerializeField] private GameObject deathVFXPrefab;

    // Private field to keep track of the current health of the object.
    private int currentHealth;

    // Reference to the Knockback script attached to the same GameObject.
    private Knockback knockback;

    // Reference to the Flash script attached to the same GameObject.
    private Flash flash;

    // Awake is called when the script instance is being loaded.
    private void Awake()
    {
        // Get the Knockback script component attached to the same GameObject.
        knockback = GetComponent<Knockback>();

        // Get the Flash script component attached to the same GameObject.
        flash = GetComponent<Flash>();
    }

    // Start is called before the first frame update.
    // Initialize the current health to the starting health value.
    private void Start()
    {
        // Set the current health to the starting health value.
        currentHealth = startingHealth;
    }

    // Method to handle taking damage.
    // Reduces the current health by the amount of damage taken and logs the current health.
    // Calls DetectDeath() to check if the object should be destroyed.
    public void TakeDamage(int damage)
    {
        // Reduce the current health by the damage amount.
        currentHealth -= damage;

        // Apply knockback force to the enemy.
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);

        // Start the flash effect coroutine.
        StartCoroutine(flash.FlashRoutine());
    }

    // Private method to detect if the object should be destroyed due to having zero or negative health.
    public void DetectDeath()
    {
        // Check if the current health is less than or equal to zero.
        if (currentHealth <= 0)
        {
            // Instantiate the death visual effects prefab at the current position with no rotation.
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);

            // Destroy the GameObject associated with this script.
            Destroy(gameObject);
        }
    }


}
