using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // Declare a private enumeration named State with a single value, Roaming
    private enum State
    {
        Roaming
    }

    // Declare private variables for the current state and the enemy's pathfinding component
    private State state;
    private EnemyPathFinding enemyPathFinding;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the EnemyPathFinding component attached to the same GameObject and assign it to enemyPathFinding
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        // Set the initial state to Roaming
        state = State.Roaming;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Start the RoamingRoutine coroutine when the script is initialized
        StartCoroutine(RoamingRoutine());
    }

    // Coroutine to handle enemy roaming behavior
    private IEnumerator RoamingRoutine()
    {
        // Continuously execute the routine while the state is set to Roaming
        while (state == State.Roaming)
        {
            // Get a random position for the enemy to roam to
            Vector2 roamPosition = GetRoamingPosition();
            // Instruct the enemy's pathfinding component to move to the random position
            enemyPathFinding.MoveTo(roamPosition);
            // Wait for a specified duration before executing the next iteration of the routine
            yield return new WaitForSeconds(2f);
        }
    }

    // Method to generate a random roaming position for the enemy
    private Vector2 GetRoamingPosition()
    {
        // Generate a random vector within a specified range and normalize it
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

}
