using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Meta.XR.MRUtilityKit.FindSpawnPositions;

public class laughSpawner : MonoBehaviour
{
    public AudioClip LaughSfx;
    public float interval;

    private GameObject[] taggedObjects;
    private Vector3 spawnLocation;


    void Start()
    {
        StartCoroutine(SpawnAudioRoutine());
        taggedObjects = GameObject.FindGameObjectsWithTag("Target");
    }

    IEnumerator SpawnAudioRoutine()
    {
        while (true) // Loop forever
        {
            int randomIndex = Random.Range(0, taggedObjects.Length);
            spawnLocation = taggedObjects[randomIndex].gameObject.transform.position;
            PlaySoundAtLocation();
            yield return new WaitForSeconds(interval); // Wait 10 seconds
        }
    }

    void PlaySoundAtLocation()
    {
        // 1. Create temporary object
        GameObject tempAudioObject = new GameObject("TempAudio");
        tempAudioObject.transform.position = spawnLocation;

        // 2. Add AudioSource component
        AudioSource aSource = tempAudioObject.AddComponent<AudioSource>();
        aSource.clip = LaughSfx;
        aSource.spatialBlend = 1.0f; // 1.0 for 3D, 0.0 for 2D
        aSource.Play();

        // 3. Destroy it after it finishes playing
        Destroy(tempAudioObject, LaughSfx.length);
    }
}
