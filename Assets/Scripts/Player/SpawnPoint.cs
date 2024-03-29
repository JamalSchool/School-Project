using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Transform spawnprf;

    private void Start()
    {
        spawnprf = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        this.gameObject.transform.position = spawnprf.position;
    }
}
