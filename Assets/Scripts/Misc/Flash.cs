using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    // Serialized field to hold the white flash material
    [SerializeField] private Material whiteFlashMat;

    // Serialized field to determine the time it takes to restore the default material after the flash
    [SerializeField] private float restoreDefaultMatTime = .2f;

    // Private field to store the default material of the sprite renderer
    private Material defaultMat;

    // Reference to the sprite renderer component of the object
    private SpriteRenderer spriteRenderer;

    // Reference to the EnemyHealth script attached to the same GameObject
    private EnemyHealth enemyHealth;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the EnemyHealth component attached to the same GameObject
        enemyHealth = GetComponent<EnemyHealth>();

        // Get the SpriteRenderer component attached to the same GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Store the default material of the sprite renderer
        defaultMat = spriteRenderer.material;
    }

    // Coroutine to handle the white flash effect
    public IEnumerator FlashRoutine()
    {
        // Set the material of the sprite renderer to the white flash material
        spriteRenderer.material = whiteFlashMat;

        // Pause the coroutine execution for a specified duration
        yield return new WaitForSeconds(restoreDefaultMatTime);

        // Restore the material of the sprite renderer to the default material
        spriteRenderer.material = defaultMat;

        // Call the DetectDeath method of the EnemyHealth script
        enemyHealth.DetectDeath();
    }
}

