using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Private field to store the starting health of the object, serialized for inspection in the Unity Inspector.
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;

    // Private field to keep track of the current health of the object.
    private int currentHealth;
    private Knockback knockback;
    private Flash flash;


    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    // Start is called before the first frame update.
    // Initialize the current health to the starting health value.
    private void Start()
    {
        currentHealth = startingHealth;
    }

    // Method to handle taking damage.
    // Reduces the current health by the amount of damage taken and logs the current health.
    // Calls DetectDeath() to check if the object should be destroyed.
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce current health by the damage amount.
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());
    }

    // Private method to detect if the object should be destroyed due to having zero or negative health.
    public void DetectDeath()
    {
        // Check if the current health is less than or equal to zero.
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            // If so, destroy the GameObject associated with this script.
            Destroy(gameObject);
        }
    }


}
