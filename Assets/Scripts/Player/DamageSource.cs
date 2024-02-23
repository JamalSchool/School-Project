using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    // Serialized field to determine the amount of damage inflicted on the enemy, settable in the Unity Inspector.
    [SerializeField] private int damageAmount = 1;

    // OnTriggerEnter2D is called when another Collider2D enters this object's trigger collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding GameObject has an EnemyHealth component attached.
        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            // If so, get the EnemyHealth component from the colliding GameObject.
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();

            // Call the TakeDamage method of the EnemyHealth component, passing the damageAmount as the parameter.
            enemyHealth.TakeDamage(damageAmount);
        }
    }

}
