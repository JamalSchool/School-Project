using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour

{
    private GameObject playerPrefab = Resources.Load<GameObject>("Player");
    private GameObject spawnPoint = Resources.Load<GameObject>("Spawn");

    

    // Start is called before the first frame update
    void Start()
    {
        // Check if both prefabs are found
        if (playerPrefab != null && spawnPoint != null)
        {
            // Instantiate the player prefab at the position of the spawn point
            Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            // Log an error message if one or both prefabs are not found
            Debug.LogError("Player or spawn point prefab not found!");
        }
    }
}


