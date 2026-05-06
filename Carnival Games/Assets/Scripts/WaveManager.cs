using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public FindSpawnPositions spawner;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (spawner.SpawnedObjects.Count == 0) return;

        // check if all clowns are destroyed
        bool allGone = true;
        foreach (GameObject obj in spawner.SpawnedObjects)
        {
            if (obj != null)
            {
                allGone = false;
                break;
            }
        }

        if (allGone)
        {
            StartCoroutine(RespawnWave());
        }
    }

    IEnumerator RespawnWave()
    {
        spawner.ClearSpawnedPrefabs();
        yield return new WaitForSeconds(1f);
        spawner.StartSpawn();
    }
}